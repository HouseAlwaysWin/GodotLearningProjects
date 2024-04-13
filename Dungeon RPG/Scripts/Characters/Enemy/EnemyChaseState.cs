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
    }

    protected override void ExitState()
    {
        timerNode.Timeout -= HandleTimeout;
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
