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


    Array Tiles = new Array();
    Array Selections = new Array();
    int TargetParis = 0;
    int MovesMade = 0;
    int PairsMade = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        this.SGManager.Connect(SignalManager.SignalName.OnTileSelected, Callable.From<MemoryTile>(OnTileSelected));
    }

    private void OnTileSelected(MemoryTile tile)
    {
        tile.Reveal(true);
        Selections.Add(tile);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
