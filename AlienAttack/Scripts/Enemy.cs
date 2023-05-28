using Godot;
using System;

public partial class Enemy : Area2D
{
    [Export]
    public float Speed = 400;

    // [Signal]
    // public delegate void EnemyDieEventHandler();

    public VisibleOnScreenNotifier2D VisibleNotifier;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        VisibleNotifier = GetNode<VisibleOnScreenNotifier2D>("VisibleNotifier");
        // VisibleNotifier.ScreenExited += EnemyScreenExited;
    }

    private void EnemyScreenExited()
    {
        if (GlobalPosition.X < 0)
        {
            QueueFree();
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _PhysicsProcess(double delta)
    {
        var moveSpeed = GlobalPosition.X - Speed * (float)delta;
        GlobalPosition = new Vector2(moveSpeed, GlobalPosition.Y);
    }

    public void Die()
    {
        QueueFree();
        // EmitSignal(SignalName.EnemyDie);
    }

    public void OnBodyEntered(Node2D body)
    {
        ((Player)body).TakePlayerDamage();
        Die();
    }
}
