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

    private Vector2 DRAG_LIM_MAX = new Vector2(0, 60);
    private Vector2 DRAG_LIM_MIN = new Vector2(-60, 0);
    private const float IMPULSE_MULT = 10f;

    [OnReady]
    public AudioStreamPlayer StretchSound;
    [OnReady]
    public AudioStreamPlayer LaunchSound;

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

    private void ReleaseIt()
    {
        _dragging = false;
        _released = true;
        Freeze = false;
        ApplyCentralImpulse(GetImpulse());
        StretchSound.Stop();
        LaunchSound.Play();
    }

    private Vector2 GetImpulse()
    {
        return _draggedVector * -1 * IMPULSE_MULT;
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

        if (_lastDragAmount > 0 && !StretchSound.Playing)
        {
            StretchSound.Play();
        }

        _draggedVector = gmp - _dragStart;
        _draggedVector.X = Mathf.Clamp(_draggedVector.X, DRAG_LIM_MIN.X, DRAG_LIM_MAX.X);
        _draggedVector.Y = Mathf.Clamp(_draggedVector.Y, DRAG_LIM_MIN.Y, DRAG_LIM_MAX.Y);
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
        else if (Input.IsActionJustReleased("drag"))
        {
            ReleaseIt();
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
        _dragging:{_dragging} _release:{_released}
        _start:{_start.ToPositionString("0.0")} _dragStart: {_dragStart.ToPositionString("0.0")} _draggedVector:{_draggedVector.ToPositionString("0.0")}
        _lastDraggedPosition:{_lastDraggedPosition.ToPositionString("0.0")} _lastDragAmount: {_lastDragAmount:0.0}
        ang:{AngularVelocity:0.0} linear: {LinearVelocity.ToPositionString("0.0")} _firedTime:{_firedTime:0.0}
        ";
        this.GameManager.EmitSignal(GameManager.SignalName.OnUpdateDebugLabel, strBuilder.ToString());
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
