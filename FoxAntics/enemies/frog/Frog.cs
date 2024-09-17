using System;
using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;

public partial class Frog : EnemyBase
{
    [OnReady]
    public AnimatedSprite2D AnimatedSprite2D;

    [OnReady]
    public Timer JumpTimer;

    [OnReady]
    public VisibleOnScreenEnabler2D VisibleOnScreenEnabler2D;

    const float JUMP_MIN_TIME = 2.0f;
    const float JUMP_MAX_TIME = 4.0f;

    const float JUMP_VELOCITY_X = 100;
    const float JUMP_VELOCITY_Y = 150;

    Vector2 JUMP_VELOCITY_R = new Vector2(JUMP_VELOCITY_X, -JUMP_VELOCITY_Y);
    Vector2 JUMP_VELOCITY_L = new Vector2(-JUMP_VELOCITY_X, -JUMP_VELOCITY_Y);

    public bool _jump;
    public bool _seenPlayer;

    public override void _Ready()
    {
        base._Ready();
        this.InitOnReady();
        JumpTimer.Timeout += OnJumpTimeOut;
        // VisibleOnScreenEnabler2D.ScreenEntered += OnVisibleOnScreenEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (IsOnFloor())
        {
            Velocity = Velocity.SetX(0);
            AnimatedSprite2D.Play("idle");
        }
        else
        {
            var y = (float)(Velocity.Y + _gravity * delta);
            Velocity = Velocity.SetY(y);
            // Velocity = new Vector2(Velocity.X, y);
        }

        ApplyJump();
        MoveAndSlide();
        FlipMe();
    }

    public void FlipMe()
    {
        if (_playerRef.GlobalPosition.X > GlobalPosition.X && !this.AnimatedSprite2D.FlipH)
        {
            this.AnimatedSprite2D.FlipH = true;
        }
        else if (_playerRef.GlobalPosition.X < GlobalPosition.X && this.AnimatedSprite2D.FlipH)
        {
            this.AnimatedSprite2D.FlipH = false;
        }
    }

    public void ApplyJump()
    {
        if (!IsOnFloor()) return;

        if (!_seenPlayer || !_jump) return;

        if (AnimatedSprite2D.FlipH)
        {
            Velocity = JUMP_VELOCITY_R;
        }
        else
        {
            Velocity = JUMP_VELOCITY_L;
        }

        _jump = false;
        this.AnimatedSprite2D.Play("jump");
        StartTimer();

    }

    public void StartTimer()
    {
        JumpTimer.WaitTime = GD.RandRange(JUMP_MIN_TIME, JUMP_MAX_TIME);
        JumpTimer.Start();
    }

    private void OnJumpTimeOut()
    {
        _jump = true;
    }


    public override void OnVisibleOnScreenEntered()
    {
        _seenPlayer = true;
        StartTimer();
    }
}
