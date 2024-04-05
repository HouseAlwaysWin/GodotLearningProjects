using Godot;
using System;

public partial class PlayerMoveState : PlayerState
{

    [Export(PropertyHint.Range, "0,20,0.1")] private int speed = 5;

    protected override void EnterState()
    {
        playerNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (playerNode.direction == Vector2.Zero)
        {
            playerNode.StateMachineNode.SwitchState<PlayerIdleState>();
            return;
        }
        playerNode.Velocity = new(playerNode.direction.X, 0, playerNode.direction.Y);
        playerNode.Velocity *= speed;
        playerNode.MoveAndSlide();
        playerNode.Flip();
    }


    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            playerNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
    }
}
