using Godot;
using System;

public partial class Exited : Area2D
{
    public AnimatedSprite2D animatedSprite2D;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        Animate();
    }

    public void Animate()
    {
        animatedSprite2D.Play("exited");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
