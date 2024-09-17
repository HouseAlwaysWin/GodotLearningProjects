using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;



public partial class EnemyBase : CharacterBody2D
{
    [Export]
    public int Points = 1;

    [OnReady("SignalManager", true)]
    public SignalManager SignalManager;

    [OnReady]
    public VisibleOnScreenEnabler2D VisibleOnScreenEnabler2D;

    public const float OFF_SCREEN_KILL_ME = 200f;
    public float _speed = 30f;
    public float _gravity = 300f;
    public Player _playerRef;
    public bool _dying = false;
    [OnReady]
    public Area2D HitBox;

    public override void _Ready()
    {
        this.InitOnReady();
        _playerRef = (Player)GetTree().GetNodesInGroup(GameManager.GROUP_PLAYER)[0];
        // VisibleOnScreenEnabler2D.Connect(VisibleOnScreenNotifier2D.SignalName.ScreenEntered, Callable.From(OnVisibleOnScreenEntered));
        // VisibleOnScreenEnabler2D.Connect(VisibleOnScreenNotifier2D.SignalName.ScreenExited, Callable.From(OnVisibleOnScreenExited));
        VisibleOnScreenEnabler2D.ScreenEntered += OnVisibleOnScreenEntered;
        HitBox.AreaEntered += OnHitBoxAreaEntered;
    }

    private void OnHitBoxAreaEntered(Area2D area)
    {
        throw new NotImplementedException();
    }

    public override void _PhysicsProcess(double delta)
    {
        FallenOff();
    }

    public void FallenOff()
    {
        if (GlobalPosition.Y > OFF_SCREEN_KILL_ME)
        {
            QueueFree();
        }
    }

    public void Die()
    {
        if (_dying) return;

        _dying = true;
        SignalManager.EmitSignal(SignalManager.SignalName.OnEnemyHit, Points, GlobalPosition);
        SetPhysicsProcess(false);
        Hide();
        QueueFree();
    }

    public virtual void OnVisibleOnScreenEntered()
    {

    }


}
