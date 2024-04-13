using Godot;
using System;

public partial class EnemyPatrolState : EnemyState
{
    [Export] private Timer idleTimerNode;
    [Export(PropertyHint.Range, "0,20,0.1")] private float maxIdleTieme = 4;
    private int pointIndex = 0;
    protected override void EnterState()
    {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);
        pointIndex = 1;

        destination = GetPointGlobalPosition(pointIndex);

        characterNode.AgentNode.TargetPosition = destination;
        characterNode.AgentNode.NavigationFinished += HandleNavigationFinished;

        idleTimerNode.Timeout += HandleTimeout;

        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        characterNode.AgentNode.NavigationFinished -= HandleNavigationFinished;
        idleTimerNode.Timeout -= HandleTimeout;
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!idleTimerNode.IsStopped())
        {
            return;
        }
        Move();
    }



    private void HandleNavigationFinished()
    {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_IDLE);

        RandomNumberGenerator rng = new();
        idleTimerNode.WaitTime = rng.RandfRange(0, maxIdleTieme);

        idleTimerNode.Start();


    }
    private void HandleTimeout()
    {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);
        pointIndex = Mathf.Wrap(
                  pointIndex + 1, 0, characterNode.PathNode.Curve.PointCount
              );

        destination = GetPointGlobalPosition(pointIndex);
        characterNode.AgentNode.TargetPosition = destination;
    }



}
