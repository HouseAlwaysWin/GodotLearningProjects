using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class Player : CharacterBody2D
{
    [OnReady]
    public Sprite2D Sprite2D;

    [OnReady]
    public CollisionShape2D CollisionShape2D;

    [OnReady]
    public AnimationPlayer AnimationPlayer;

    private const float GRAVITY = 300f;

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
            MoveAndSlide();
        }
    }
}
