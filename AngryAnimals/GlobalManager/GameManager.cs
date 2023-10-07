using Godot;
using System;

public partial class GameManager : Node
{
    [Signal]
    public delegate void OnUpdateDebugLabelEventHandler(string text);

    [Signal]
    public delegate void OnAnimailDiedEventHandler();

}
