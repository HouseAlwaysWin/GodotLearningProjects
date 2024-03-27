using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{

    [Export] private Node currentState;
    [Export] private Node[] states;
    public override void _Ready()
    {
        currentState.Notification(5001);
    }

    public void SwitchState<T>()
    {
        Node newState = states.FirstOrDefault(s => s is T);
        // foreach (Node state in states)
        // {
        //     if (state is T)
        //     {
        //         newState = state;
        //     }
        // }

        if (newState == null) return;

        currentState = newState;
        currentState.Notification(5001);
    }


}
