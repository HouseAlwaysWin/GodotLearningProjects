using Godot;

public abstract partial class PlayerState : Node
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
            playerNode.StateMachineNode.SwitchState<PlayerMoveState>();
        }
    }




    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == GameConstants.NOTIFICATION_ENTER_STATE)
        {
            EnterState();
            SetPhysicsProcess(true);
            SetProcessInput(true);
        }
        else if (what == GameConstants.NOTIFICATION_EXIT_STATE)
        {
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }
    }

    protected virtual void EnterState()
    {
        playerNode.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
    }





}