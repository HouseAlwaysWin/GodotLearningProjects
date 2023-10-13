using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

public partial class Animal : RigidBody2D
{
    [OnReady("/root/GameManager")]
    public GameManager GameManager;

    [OnReady]
    public VisibleOnScreenNotifier2D VisibleOnScreenNotifier2D;

    private bool _dead = false;
    private bool _dragging = false;
    private bool _released = false;
    private Vector2 _start = Vector2.Zero;
    private Vector2 _dragStart = Vector2.Zero;
    private Vector2 _draggedVector = Vector2.Zero;
    private Vector2 _lastDraggedPosition = Vector2.Zero;
    private float _lastDragAmount = 0.0f;
    private float _firedTime = 0.0f;



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        this.VisibleOnScreenNotifier2D.ScreenExited += OnScreenExited;
        this.InputEvent += OnInputEvent;
        _start = GlobalPosition;
    }

    private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
    {
        if (@event.IsActionPressed("drag"))
        {
            GD.Print(@event);
        }
    }

    private void OnScreenExited()
    {
        Died();
    }

    private void Died()
    {
        if (_dead)
        {
            return;
        }
        _dead = true;
        this.GameManager.EmitSignal(GameManager.SignalName.OnAnimailDied);
        QueueFree();
    }

    public override void _PhysicsProcess(double delta)
    {

        this.InitOnReady();
        UpdateDebugLabel();
    }

    private void UpdateDebugLabel()
    {
        StringBuilder strBuilder = new($@"g_pos:{GlobalPosition.ToPositionString("0.0")}");
        strBuilder.Append($"\ndragging:{_dragging.ToString} released: {_released}");
        strBuilder.Append($"\nang:{AngularVelocity.ToString("0.0")} linear: {LinearVelocity.ToPositionString("0.0")}");
        strBuilder.Append($"\nang:{AngularVelocity.ToString("0.0")} linear: {LinearVelocity.ToPositionString("0.0")}");
        this.GameManager.EmitSignal(GameManager.SignalName.OnUpdateDebugLabel, strBuilder.ToString());
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
