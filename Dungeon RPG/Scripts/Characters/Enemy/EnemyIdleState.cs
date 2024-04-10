using Godot;
using System;

public partial class EnemyIdleState : EnemyState
{
    protected override void EnterState()
    {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
    }
}
