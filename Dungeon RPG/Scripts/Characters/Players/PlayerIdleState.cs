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
        }
    }
}
