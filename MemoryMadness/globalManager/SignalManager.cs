using Godot;

public partial class SignalManager : Node
{
    [Signal]
    public delegate void OnLevelSelectedEventHandler(int levelNum);

    [Signal]
    public delegate void OnGameExitPressedEventHandler();

    [Signal]
    public delegate void OnSelectionEnabledEventHandler();

    [Signal]
    public delegate void OnSelectionDisabledEventHandler();

    [Signal]
    public delegate void OnTileSelectedEventHandler(MemoryTile tile);

    [Signal]
    public delegate void OnGameOverEventHandler(int moves);
}
