using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

public partial class Player : CharacterBody2D
{
    [OnReady]
    public Sprite2D Sprite2D;

    [OnReady]
    public CollisionShape2D CollisionShape2D;

    [OnReady]
    public AnimationPlayer AnimationPlayer;

    private const float GRAVITY = 300f;
    private const float RUN_SPEED = 100f;
    private const float JUMP_VELOCITY = -400f;
    private const float MAX_FALL = 400f;
    private const float HURT_TIME = 0.3f;

    public override void _Ready()
    {
        this.InitOnReady();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!IsOnFloor())
        {
            float y = Velocity.Y + (GRAVITY * (float)delta);
            Velocity = new Vector2(Velocity.X, y);
        }

        GetInput();
        MoveAndSlide();
    }

    public void GetInput()
    {
        Velocity = new Vector2(0, Velocity.Y);
        if (Input.IsActionPressed("jump") && IsOnFloor())
        {
            Velocity = new Vector2(Velocity.X, JUMP_VELOCITY);
        }

        if (Input.IsActionPressed("left"))
        {
            Velocity = new Vector2(-RUN_SPEED, Velocity.Y);
        }
        if (Input.IsActionPressed("right"))
        {
            Velocity = new Vector2(RUN_SPEED, Velocity.Y);
        }

        var y = Mathf.Clamp(Velocity.Y, JUMP_VELOCITY, MAX_FALL);
        Velocity = new Vector2(Velocity.X, y);

    }
}
