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
        this.InitOnReady();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (IsOnFloor())
        {
            var currentX = _speed * (float)_facing;
            Velocity = new Vector2(currentX, Velocity.Y);
        }
        else
        {
            var currentY = _gravity * (float)delta;
            Velocity = new Vector2(Velocity.X, currentY);
        }

        MoveAndSlide();

        if (IsOnWall() || !FloorDetection.IsColliding())
        {
            FlipMe();
        }
    }

    private void FlipMe()
    {
        AnimatedSprite2D.FlipH = !AnimatedSprite2D.FlipH;
        FloorDetection.Position = new Vector2(FloorDetection.Position.X * -1, FloorDetection.Position.Y);

        if (_facing == FACING.LEFT)
        {
            _facing = FACING.RIGHT;
        }
        else
        {
            _facing = FACING.LEFT;
        }

    }
}
