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

        characterNode.AttackAreaNode.BodyExited += HandleAttackAreaBodyExited;
        characterNode.AnimationPlayer.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAttackAreaBodyExited(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
    }

    protected override void ExitState()
    {
        characterNode.AttackAreaNode.BodyExited -= HandleAttackAreaBodyExited;
        characterNode.AnimationPlayer.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        characterNode.ToggleHitbox(true);

        Node3D target = characterNode.AttackAreaNode.GetOverlappingBodies().First();

        if (target == null)
        {
            Node3D chaseTarget = characterNode.ChaseAreaNode.GetOverlappingBodies().FirstOrDefault();
            if (chaseTarget == null)
            {
                characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
            }

            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
            return;
        }
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_ATTACK);
        targetPosition = target.GlobalPosition;

        Vector3 direction = characterNode.GlobalPosition.DirectionTo(targetPosition);
        characterNode.Sprite3D.FlipH = direction.X < 0;

    }

    private void PerformHit()
    {
        characterNode.ToggleHitbox(false);
        characterNode.HitboxNode.GlobalPosition = targetPosition;
    }

}
