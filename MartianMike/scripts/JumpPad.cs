using Godot;
using System;

public partial class JumpPad : Area2D
{

    public AnimatedSprite2D animatedSprite2D;

    [Export]
    public float JumpForce = 300;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        BodyEntered += JumpPadBodyEntered;
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    private void JumpPadBodyEntered(Node2D body)
    {
        if (body is Player)
        {
            animatedSprite2D.Play("Jump");
            var player = (Player)body;
            player.Jump(JumpForce);
        }
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }


}
