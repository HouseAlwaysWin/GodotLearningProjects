using Godot;
using System;

public partial class GameManager : Node
{
    [Signal]
    public delegate void OnUpdateDebugLabelEventHandler(string text);

    [Signal]
    public delegate void OnAnimailDiedEventHandler();

    [Signal]
    public delegate void OnCupDestroyedEventHandler();

    public string GROUP_CUP = "cup";
    public string GROUP_ANIMAL = "animal";
    



}
