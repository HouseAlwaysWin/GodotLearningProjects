using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Node")]
    [Export] public AnimationPlayer AnimationPlayer { get; private set; }
    [Export] public Sprite3D Sprite3D { get; private set; }
    [Export] public StateMachine StateMachineNode { get; private set; }
    public Vector2 direction = new();

    // public override void _PhysicsProcess(double delta)
    // {
    //     Velocity = new(direction.X, 0, direction.Y);
    //     Velocity *= 5;
    //     MoveAndSlide();
    //     Flip();
    // }

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT,
            GameConstants.INPUT_MOVE_RIGHT,
            GameConstants.INPUT_MOVE_FORWARD,
            GameConstants.INPUT_MOVE_BACKWARD
        );

        // if (direction == Vector2.Zero)
        // {
        //     animationPlayer.Play(GameConstants.ANIM_IDLE);
        // }
        // else
        // {
        //     animationPlayer.Play(GameConstants.ANIM_MOVE);
        // }
    }

    public void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;

        if (isNotMovingHorizontally) return;

        bool isMovingLeft = Velocity.X < 0;
        Sprite3D.FlipH = isMovingLeft;
    }
}
