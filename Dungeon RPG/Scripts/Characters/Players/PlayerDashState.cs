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
        playerNode.Velocity = Vector3.Zero;
        playerNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }

    public override void _PhysicsProcess(double delta)
    {
        playerNode.MoveAndSlide();
        playerNode.Flip();
    }



    protected override void EnterState()
    {
        playerNode.AnimationPlayer.Play(GameConstants.ANIM_DASH);
        playerNode.Velocity = new(playerNode.direction.X, 0, playerNode.direction.Y);

        if (playerNode.Velocity == Vector3.Zero)
        {
            playerNode.Velocity = playerNode.Sprite3D.FlipH ? Vector3.Left : Vector3.Right;
        }

        playerNode.Velocity *= speed;
        dashTimerNode.Start();
    }
}
