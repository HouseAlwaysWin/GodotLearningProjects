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

    public bool IsActivated = true;

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

        float direction = 0;

        if (IsActivated)
        {
            if (Input.IsActionJustPressed("jump"))
            {
                // Velocity = new Vector2(Velocity.X, -JumpForce);
                Jump(JumpForce);
            }

            direction = Input.GetAxis("move_left", "move_right");
            if (direction != 0)
            {
                animatedSprite2D.FlipH = (direction == -1);
            }
        }

        Velocity = new Vector2(direction * Speed, Velocity.Y);
        MoveAndSlide();
        UpdateAnimations(direction);
    }

    public void Jump(float force)
    {
        Velocity = new Vector2(Velocity.X, -force);
    }

    public void UpdateAnimations(float direction)
    {
        if (IsOnFloor())
        {
            if (direction == 0)
            {
                animatedSprite2D.Play("Idle");
            }
            else
            {
                animatedSprite2D.Play("Run");
            }
        }
        else
        {
            if (Velocity.Y < 0)
            {
                animatedSprite2D.Play("Jump");
            }
            else
            {
                animatedSprite2D.Play("Fall");
            }
        }
    }


}
