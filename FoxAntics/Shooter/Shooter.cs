using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class Shooter : Node2D
{
    [OnReady]
    public Timer ShootTimer;

    [OnReady]
    public AudioStreamPlayer2D Sound;

    [Export]
    public float Speed = 50.0f;

    [Export]
    public float LifeSpan = 10.0f;

    [Export]
    public ObjectType BulletKey = ObjectType.BULLET_PLAYER;

    [Export]
    public float ShootDelay = 0.7f;

    private bool _canShoot = true;

    [OnReady(isAutoLoad: true)]
    public SignalManager _signalManager;


    public override void _Ready()
    {
        this.InitOnReady();
        ShootTimer.WaitTime = ShootDelay;
        ShootTimer.Timeout += OnShooterTimeout;
    }


    private void OnShooterTimeout()
    {
        _canShoot = true;
    }

    public void Shoot(Vector2 direction)
    {
        if (!_canShoot) return;
        _canShoot = false;
        try
        {
            this._signalManager.EmitSignal(SignalManager.SignalName.OnCreateBullet,
                GlobalPosition,
                direction,
                LifeSpan,
                Speed,
                (int)BulletKey
            );

            ShootTimer.Start();
        }
        catch (Exception ex)
        {
            GD.Print(ex);
        }
    }

}
