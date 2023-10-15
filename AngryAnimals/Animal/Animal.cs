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
        if (_dragging || _released)
        {
            return;
        }

        if (@event.IsActionPressed("drag"))
        {
            GrabIt();
        }
    }

    private void OnScreenExited()
    {
        Died();
    }

    private void GrabIt()
    {
        _dragging = true;
        _dragStart = GetGlobalMousePosition();
        _lastDraggedPosition = _dragStart;
    }

    private void DragIt()
    {
        var gmp = GetGlobalMousePosition();
        _lastDragAmount = (_lastDraggedPosition - gmp).Length();
        _lastDraggedPosition = gmp;

        _draggedVector = gmp - _dragStart;
        GlobalPosition = _start + _draggedVector;
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
        UpdateDebugLabel();

        if (_released)
        {
            return;
        }
        else if (!_dragging)
        {
            return;
        }
        else
        {
            DragIt();
        }
    }

    private void UpdateDebugLabel()
    {
        string strBuilder = $@"
        g_pos:{GlobalPosition.ToPositionString("0.0")}
        \n_dragging:{_dragging} _release:{_released}
        \n_start:{_start.ToPositionString("0.0")} _dragStart: {_dragStart.ToPositionString("0.0")} _draggedVector:{_draggedVector.ToPositionString("0.0")}
        \n_lastDraggedPosition:{_lastDraggedPosition.ToPositionString("0.0")} _lastDragAmount: {_lastDragAmount:0.0}
        \nang:{AngularVelocity:0.0} linear: {LinearVelocity.ToPositionString("0.0")} _firedTime:{_firedTime:0.0}
        ";
        this.GameManager.EmitSignal(GameManager.SignalName.OnUpdateDebugLabel, strBuilder.ToString());
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
