using Godot;
using System;

public partial class PlayerMoveState : PlayerState
{
    protected override void EnterState()
    {
        playerNode.animationPlayer.Play(GameConstants.ANIM_MOVE);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (playerNode.direction == Vector2.Zero)
        {
            playerNode.stateMachineNode.SwitchState<PlayerIdleState>();
            return;
        }
        playerNode.Velocity = new(playerNode.direction.X, 0, playerNode.direction.Y);
        playerNode.Velocity *= 5;
        playerNode.MoveAndSlide();
        playerNode.Flip();
    }


    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            playerNode.stateMachineNode.SwitchState<PlayerDashState>();
        }
    }
}
