using Godot;
using System;

public partial class SignalManager : Node
{
    [Signal]
    public delegate void OnLevelSelectedEventHandler(int levelNum);

    [Signal]
    public delegate void OnGameExitPressedEventHandler();
}
