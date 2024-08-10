using System;
using Godot;

public partial class EnemyState : CharacterState
{
    protected Vector3 destination;

    public override void _Ready()
    {
        base._Ready();
        this.characterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }

    private void HandleZeroHealth()
    {
        characterNode.StateMachineNode.SwitchState<EnemyDeathState>();
    }

    protected Vector3 GetPointGlobalPosition(int index)
    {
        Vector3 localPos = characterNode.PathNode.Curve.GetPointPosition(index);
        Vector3 globalPos = characterNode.PathNode.GlobalPosition;
        return localPos + globalPos;
    }

    protected void Move()
    {
        characterNode.AgentNode.GetNextPathPosition();
        characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(destination);
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    protected void HandleChaseAreaBodyEntered(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
    }





}