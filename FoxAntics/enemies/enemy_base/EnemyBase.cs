using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;
using System.Threading;



public abstract partial class EnemyBase : CharacterBody2D
{
    [Export]
    public FACING DefaultFacing = FACING.LEFT;

    [Export]
    public int Points = 1;

    [OnReady("SignalManager", true)]
    public SignalManager SignalManager;

    [OnReady]
    public VisibleOnScreenEnabler2D VisibleOnScreenEnabler2D;

    public const float OFF_SCREEN_KILL_ME = 1000f;

    public float _speed = 30f;
    public float _gravity = 800f;
    public FACING _facing { get => DefaultFacing; }
    public Player _playerRef;
    public bool _dying = false;

    public override void _Ready()
    {
        this.InitOnReady();
        _playerRef = (Player)GetTree().GetNodesInGroup(GameManager.GROUP_PLAYER)[0];
        VisibleOnScreenEnabler2D.ScreenEntered += OnVisibleOnScreenEntered;
        VisibleOnScreenEnabler2D.ScreenExited += OnVisibleOnScreenExited;
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

    public void OnVisibleOnScreenEntered()
    {

    }

    public void OnVisibleOnScreenExited()
    {

    }




}
