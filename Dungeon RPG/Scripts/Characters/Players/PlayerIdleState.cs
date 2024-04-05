using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class PlayerIdleState : PlayerState
{
    protected override void EnterState()
    {
        playerNode.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            playerNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
    }
}
