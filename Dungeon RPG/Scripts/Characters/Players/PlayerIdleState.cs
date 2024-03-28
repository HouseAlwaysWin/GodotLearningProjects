using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class PlayerIdleState : Node
{
    private Player playerNode;
    public override void _Ready()
    {
        playerNode = GetOwner<Player>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (playerNode.direction != Vector2.Zero)
        {
            playerNode.stateMachineNode.SwitchState<PlayerMoveState>();
        }
    }


    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == 5001)
        {
            playerNode.animationPlayer.Play(GameConstants.ANIM_IDLE);
            SetPhysicsProcess(true);
            SetProcessInput(true);
        }
        else if (what == 5002)
        {
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            playerNode.stateMachineNode.SwitchState<PlayerDashState>();
        }
    }
}
