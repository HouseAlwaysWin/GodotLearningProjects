using Godot;
using System;

public partial class PlayerMoveState : Node
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
        if (playerNode.direction == Vector2.Zero)
        {
            playerNode.stateMachineNode.SwitchState<PlayerIdleState>();
        }
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == 5001)
        {
            Player playerNode = GetOwner<Player>();
            playerNode.animationPlayer.Play(GameConstants.ANIM_MOVE);
            SetPhysicsProcess(true);
            SetProcessInput(true);
        }
        else if (what == 5002)
        {
            SetProcessInput(false);
            SetPhysicsProcess(false);
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
