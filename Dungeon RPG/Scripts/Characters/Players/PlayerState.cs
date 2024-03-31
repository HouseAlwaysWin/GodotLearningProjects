using Godot;

public partial class PlayerState : Node
{
    protected Player playerNode;
    public override void _Ready()
    {
        playerNode = GetOwner<Player>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (playerNode.direction != Vector2.Zero)
        {
            playerNode.stateMachineNode.SwitchState<PlayerMoveState>();
        }
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == 5001)
        {
            EnterState();
            SetPhysicsProcess(true);
            SetProcessInput(true);
        }
        else if (what == 5002)
        {
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }
    }

    protected virtual void EnterState()
    {
        playerNode.animationPlayer.Play(GameConstants.ANIM_IDLE);
    }





}