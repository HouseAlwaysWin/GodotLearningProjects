using System;
using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;

public partial class Eagle : EnemyBase
{
    [OnReady]
    public AnimatedSprite2D AnimatedSprite2D;

    [OnReady]
    public Timer DirectionTimer;

    [OnReady]
    public RayCast2D PlayerDetector;

    [OnReady]
    public Shooter Shooter;

    public Vector2 FLY_SPEED = new(35, 15);

    Vector2 _flyDirection = Vector2.Zero;

    public override void _Ready()
    {
        base._Ready();
        this.InitOnReady();
        this.DirectionTimer.Timeout += OnDirectionTimerTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Velocity = _flyDirection;
        MoveAndSlide();
        Shoot();
    }

    private void Shoot()
    {
        if (PlayerDetector.IsColliding())
        {
            Shooter.Shoot(
                GlobalPosition.DirectionTo(
                    _playerRef.GlobalPosition
                )
            );
        }
    }

    public void FlyToPlayer()
    {
        var xDir = Math.Sign(_playerRef.GlobalPosition.X - GlobalPosition.X);
        if (xDir > 0)
        {
            AnimatedSprite2D.FlipH = true;
        }
        else
        {
            AnimatedSprite2D.FlipH = false;
        }
        _flyDirection = new Vector2(xDir, 1) * FLY_SPEED;
    }

    public void OnDirectionTimerTimeout()
    {
        FlyToPlayer();
    }

    public override void OnVisibleOnScreenEntered()
    {
        AnimatedSprite2D.Play("fly");
        DirectionTimer.Start();
        FlyToPlayer();
    }
}
