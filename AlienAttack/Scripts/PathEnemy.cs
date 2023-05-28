using Godot;
using System;

public partial class PathEnemy : Path2D
{

    public Enemy Enemy;
    public PathFollow2D PathFollow2D;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        PathFollow2D = GetNode<PathFollow2D>("PathFollow2D");
        Enemy = GetNode<Enemy>("PathFollow2D/Enemy");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        PathFollow2D.ProgressRatio -= 0.25f * (float)delta;
        if (PathFollow2D.ProgressRatio <= 0)
        {
            QueueFree();
        }
    }
}
