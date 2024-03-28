using Godot;
using System;

public partial class PlayerDashState : Node
{
    private Player playerNode;
    public override void _Ready()
    {
        playerNode = GetOwner<Player>();
        playerNode.animationPlayer.Play(GameConstants.ANIM_DASH);
        SetPhysicsProcess(false);
    }

    public override void _PhysicsProcess(double delta)
    {
    }



    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == 5001)
        {
            playerNode.animationPlayer.Play(GameConstants.ANIM_IDLE);
            SetPhysicsProcess(true);
        }
        else if (what == 5002)
        {
            SetPhysicsProcess(false);
        }
    }
}
