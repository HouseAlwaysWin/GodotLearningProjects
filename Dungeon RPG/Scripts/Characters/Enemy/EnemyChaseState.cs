using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{
    private CharacterBody3D target;
    [Export] private Timer timerNode;

    protected override void EnterState()
    {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);
        target = characterNode.ChaseAreaNode.GetOverlappingBodies().First() as CharacterBody3D;

        timerNode.Timeout += HandleTimeout;
        characterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
        characterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
    }



    protected override void ExitState()
    {
        timerNode.Timeout -= HandleTimeout;
        characterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
        characterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
    }

    protected void HandleChaseAreaBodyExited(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
    }

    protected void HandleAttackAreaBodyEntered(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
    }

    private void HandleTimeout()
    {
        destination = target.GlobalPosition;
        characterNode.AgentNode.TargetPosition = destination;

    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
    }



}
