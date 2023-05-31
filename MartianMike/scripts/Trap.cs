using Godot;
using System;

public partial class Trap : Node2D
{

    [Signal]
    public delegate void TouchedPlayerEventHandler();

    public Area2D area2D;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        area2D = GetNode<Area2D>("Area2D");
        area2D.BodyEntered += OnArea2DBodyEntered;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }


    public void OnArea2DBodyEntered(Node2D body)
    {
        if (body is Player)
        {
            EmitSignal(SignalName.TouchedPlayer);
            var player = (Player)body;
        }
    }
}
