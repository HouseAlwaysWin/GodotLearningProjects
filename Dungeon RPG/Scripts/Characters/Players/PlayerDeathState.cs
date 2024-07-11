using Godot;
using System;

public partial class PlayerDeathState : PlayerState
{
    public override void _EnterTree()
    {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_DEATH);
        characterNode.AnimationPlayer.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        characterNode.QueueFree();
    }
}
