using Godot;
using System;

public partial class EnemyAttackState : EnemyState
{
    protected override void EnterState()
    {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_ATTACK);
        characterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
        characterNode.AttackAreaNode.BodyExited += HandleChaseAreaBodyExited;
    }

    protected override void ExitState()
    {
        characterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
        characterNode.AttackAreaNode.BodyExited -= HandleChaseAreaBodyExited;
    }

}
