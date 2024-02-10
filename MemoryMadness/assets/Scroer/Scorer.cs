using Godot;
using Godot.Collections;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;

public partial class Scorer : Node
{


    [OnReady("Sound")]
    public AudioStreamPlayer Sound;

    [OnReady("RevealTimer")]
    public Timer RevealTimer;

    [OnReady("/root/SignalManager")]
    public SignalManager SGManager;

    [OnReady("/root/SoundManager")]
    public SoundManager SoundManager;
    [OnReady("/root/GameManager")]
    public GameManager GameManager;


    Array Tiles = new Array();
    Array<MemoryTile> Selections = new Array<MemoryTile>();
    int TargetParis = 0;
    int MovesMade = 0;
    int PairsMade = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        this.SGManager.Connect(SignalManager.SignalName.OnTileSelected, Callable.From<MemoryTile>(OnTileSelected));
        this.SGManager.Connect(SignalManager.SignalName.OnGameExitPressed, Callable.From(OnGameExitPressed));
        this.RevealTimer.Timeout += OnRevealTimerTimeout;
    }

    private void OnGameExitPressed()
    {
        this.RevealTimer.Stop();
    }

    private void OnTileSelected(MemoryTile tile)
    {
        this.SoundManager.PlayTileClick(Sound);
        CheckPairMade(tile);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void ClearNewGame(int targetPairs)
    {
        Selections.Clear();
        PairsMade = 0;
        MovesMade = 0;
        TargetParis = targetPairs;
        Tiles = (Array)GetTree().GetNodesInGroup(this.GameManager.GROUP_TILE);
    }

    public void UpdateSelections()
    {
        RevealTimer.Start();
    }

    public void CheckPairMade(MemoryTile tile)
    {

        tile.Reveal(true);
        Selections.Add(tile);
        if (Selections.Count != 2)
        {
            return;
        }
        this.SGManager.EmitSignal(SignalManager.SignalName.OnSelectionDisabled);
        MovesMade += 1;

        UpdateSelections();
    }

    public void HideSelections()
    {

        foreach (var s in Selections)
        {
            s.Reveal(false);
        }
    }

    public void OnRevealTimerTimeout()
    {
        HideSelections();
        Selections.Clear();
        this.SGManager.EmitSignal(SignalManager.SignalName.OnSelectionEnabled);
    }
}
