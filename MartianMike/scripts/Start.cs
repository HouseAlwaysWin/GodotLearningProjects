using Godot;
using System;

public partial class Start : StaticBody2D
{

    public Marker2D StartPos;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StartPos = GetNode<Marker2D>("SpawnPosition");
    }

    public Vector2 GetSpawnPos()
    {
        return StartPos.GlobalPosition;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
