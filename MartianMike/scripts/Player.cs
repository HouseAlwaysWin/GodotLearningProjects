using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public AnimatedSprite2D animatedSprite2D;

    [Export]
    public float Gravity = 400;

    [Export]
    public float JumpForce = 200;

    [Export]
    public float Speed = 125;

    [Export]
    public float FallDownSpeed = 500;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

    public override void _PhysicsProcess(double delta)
    {
        if (!IsOnFloor())
        {
            var velocityY = Velocity.Y + Gravity * (float)delta;
            Velocity = new Vector2(Velocity.X, velocityY);
            if (Velocity.Y > FallDownSpeed)
            {
                Velocity = new Vector2(Velocity.X, FallDownSpeed);
            }
        }

        if (Input.IsActionJustPressed("jump"))
        {
            Velocity = new Vector2(Velocity.X, -JumpForce);
        }

        var direction = Input.GetAxis("move_left", "move_right");
        Velocity = new Vector2(direction * Speed, Velocity.Y);
        MoveAndSlide();
    }


}
