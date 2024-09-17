using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Runtime.Intrinsics.X86;

public partial class Snail : EnemyBase
{
    [OnReady]
    public RayCast2D FloorDetection;
    [OnReady]
    public AnimatedSprite2D AnimatedSprite2D;
    public override void _Ready()
    {
        base._Ready();
        this.InitOnReady();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (IsOnFloor())
        {
            var currentX = (this.AnimatedSprite2D.FlipH ? _speed : -_speed);
            Velocity = new Vector2(currentX, Velocity.Y);
        }
        else
        {
            var currentY = Velocity.Y + _gravity * (float)delta;
            Velocity = new Vector2(Velocity.X, currentY);
        }

        MoveAndSlide();
        if (IsOnFloor())
        {
            if (IsOnWall()
            || !FloorDetection.IsColliding()
            )
            {
                FlipMe();
            }
        }
    }

    private void FlipMe()
    {
        AnimatedSprite2D.FlipH = !AnimatedSprite2D.FlipH;
        // FloorDetection.Position = new Vector2(FloorDetection.Position.X * -1, FloorDetection.Position.Y);
        FloorDetection.Position = FloorDetection.Position.SetX(-FloorDetection.Position.X);
    }
}
