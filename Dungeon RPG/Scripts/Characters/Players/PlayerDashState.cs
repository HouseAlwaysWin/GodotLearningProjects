using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer dashTimerNode;
    [Export(PropertyHint.Range, "0,200")] private float speed = 10;

    public override void _Ready()
    {
        base._Ready();
        dashTimerNode.Timeout += HandleDashTimeout;
    }

    private void HandleDashTimeout()
    {
        characterNode.Velocity = Vector3.Zero;
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }



    protected override void EnterState()
    {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_DASH);
        characterNode.Velocity = new(characterNode.direction.X, 0, characterNode.direction.Y);

        if (characterNode.Velocity == Vector3.Zero)
        {
            characterNode.Velocity = characterNode.Sprite3D.FlipH ? Vector3.Left : Vector3.Right;
        }

        characterNode.Velocity *= speed;
        dashTimerNode.Start();
    }
}
