using Godot;
using System;

public partial class PlayerKickState : PlayerState
{

    [Export] private Timer comboCounterNode;
    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready()
    {
        base._Ready();
        comboCounterNode.Timeout += () => comboCounter = 1;
    }


    protected override void EnterState()
    {
        characterNode.AnimationPlayer.Play(GameConstants.ANIM_ATTACK + comboCounter, -1, 1.5f);
        characterNode.AnimationPlayer.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        characterNode.AnimationPlayer.AnimationFinished -= HandleAnimationFinished;
        comboCounterNode.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        comboCounter++;

        comboCounter = Mathf.Wrap(comboCounter, 1, maxComboCount + 1);

        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();

    }
}
