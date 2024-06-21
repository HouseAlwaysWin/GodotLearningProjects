using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : EnemyState
{
    private Vector3 targetPosition;
    protected override void EnterState()
    {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_ATTACK);

        Node3D target = characterNode.AttackAreaNode.GetOverlappingBodies().First();
        targetPosition = target.GlobalPosition;

        characterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
        characterNode.AttackAreaNode.BodyExited += HandleChaseAreaBodyExited;
    }

    protected override void ExitState()
    {
        characterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
        characterNode.AttackAreaNode.BodyExited -= HandleChaseAreaBodyExited;
    }

    private void PerformHit()
    {
        characterNode.ToggleHitbox(false);
        characterNode.HitboxNode.GlobalPosition = targetPosition;
    }

}
