using Godot;
using System;

public partial class Rocket : Area2D
{
    [Export]
    public float Speed = 500;

    public VisibleOnScreenNotifier2D VisibleNotifier;

    [Signal]
    public delegate void ScoreEnemyEventHandler();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        VisibleNotifier = GetNode<VisibleOnScreenNotifier2D>("VisibleNotifier");
        VisibleNotifier.ScreenExited += RocketScreenExited;
    }

    private void RocketScreenExited()
    {
        QueueFree();
    }



    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _PhysicsProcess(double delta)
    {
        var moveSpeed = GlobalPosition.X + Speed * (float)delta;
        GlobalPosition = new Vector2(moveSpeed, GlobalPosition.Y);
    }

    public void OnAreaEntered(Area2D area2D)
    {
        QueueFree();
        ((Enemy)area2D).Die();
        EmitSignal(SignalName.ScoreEnemy);
    }


}
