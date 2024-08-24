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

    [OnReady]
    public Sprite2D ArrowSprite;

    private Vector2 DRAG_LIM_MAX = new Vector2(0, 60);
    private Vector2 DRAG_LIM_MIN = new Vector2(-60, 0);
    private const float IMPULSE_MULT = 10f;
    private const float IMPULSE_MAX = 1200f;
    private const float FIRE_DELAY = 0.25f;
    private const float STOPPED = 0.1f;

    [OnReady]
    public AudioStreamPlayer StretchSound;
    [OnReady]
    public AudioStreamPlayer LaunchSound;
    [OnReady]
    public AudioStreamPlayer CollisionSound;

    private bool _dead = false;
    private bool _dragging = false;
    private bool _released = false;
    private Vector2 _start = Vector2.Zero;
    private Vector2 _dragStart = Vector2.Zero;
    private Vector2 _draggedVector = Vector2.Zero;
    private Vector2 _lastDraggedPosition = Vector2.Zero;
    private float _lastDragAmount = 0.0f;
    private float _firedTime = 0.0f;
    private float _arrowScaleX = 0.0f;
    private int _lastCollisionCount = 0;



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        this.VisibleOnScreenNotifier2D.ScreenExited += OnScreenExited;
        this.InputEvent += OnInputEvent;
        _start = GlobalPosition;
        _arrowScaleX = ArrowSprite.Scale.X;
        ArrowSprite.Hide();
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
        this.GameManager.AttemptMade();
        ArrowSprite.Hide();
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
        ArrowSprite.Show();
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
        ScaleArrow();
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
            _firedTime += (float)delta;
            if (_firedTime > FIRE_DELAY)
            {
                PlayCollision();
                CheckOnTarget();
            }
            // return;
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
        g_pos:{GlobalPosition.ToPositionString("0.0")} contacts: {GetContactCount()}
        _dragging:{_dragging} _release:{_released}
        _start:{_start.ToPositionString("0.0")} _dragStart: {_dragStart.ToPositionString("0.0")} _draggedVector:{_draggedVector.ToPositionString("0.0")}
        _lastDraggedPosition:{_lastDraggedPosition.ToPositionString("0.0")} _lastDragAmount: {_lastDragAmount:0.0}
        ang:{AngularVelocity:0.0} linear: {LinearVelocity.ToPositionString("0.0")} _firedTime:{_firedTime:0.0}
        ";
        this.GameManager.EmitSignal(GameManager.SignalName.OnUpdateDebugLabel, strBuilder.ToString());
    }

    public void ScaleArrow()
    {
        var impLen = GetImpulse().Length();
        var perc = impLen / IMPULSE_MAX;
        ArrowSprite.Scale = ArrowSprite.Scale.SetX((_arrowScaleX * perc) + _arrowScaleX);
        ArrowSprite.Rotation = (_start - GlobalPosition).Angle();
    }

    private bool StoppedRolling()
    {
        if (GetContactCount() > 0)
        {
            if (Math.Abs(LinearVelocity.Y) < STOPPED &&
               Math.Abs(AngularVelocity) < STOPPED)
            {
                return true;
            }
        }
        return false;
    }

    public void CheckOnTarget()
    {
        if (!StoppedRolling())
        {
            return;
        }

        var cb = GetCollidingBodies();
        if (cb.Count == 0)
        {
            return;
        }

        var cup = (Cup)cb[0];

        if (cup.IsInGroup(this.GameManager.GROUP_CUP))
        {
            cup.Die();
            Died();
        }
    }

    private void PlayCollision()
    {
        if (_lastCollisionCount == 0 && GetContactCount() > 0 && !CollisionSound.Playing)
        {
            CollisionSound.Play();
            return;
        }
        _lastCollisionCount = GetContactCount();
    }



    // Called every frame. 'delta' is the elapsed time since the previous frame.


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
