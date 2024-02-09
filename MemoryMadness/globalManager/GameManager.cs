using System.Diagnostics.Metrics;
using System.Linq;
using Godot;
using Godot.Collections;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;

public partial class LevelPos : Node
{
    public int Rows { get; set; }
    public int Cols { get; set; }
}

public partial class GameManager : Node
{
    [OnReady("/root/ImageManager")]
    public ImageManager ImageManager;

    public string GROUP_TILE = "tile";

    public override void _Ready()
    {
        this.InitOnReady();
    }
    public static Dictionary<int, LevelPos> LEVELS = new()
    {
        { 1 , new LevelPos{ Rows = 2,Cols = 2}},
        { 2 , new LevelPos{ Rows = 3,Cols = 4}},
        { 3 , new LevelPos{ Rows = 4,Cols = 4}},
        { 4 , new LevelPos{ Rows = 4,Cols = 6}},
        { 5 , new LevelPos{ Rows = 5,Cols = 6}},
        { 6 , new LevelPos{ Rows = 6,Cols = 6}},
    };

    public Dictionary GetLevelSelection(int levelNum)
    {
        var lData = LEVELS[levelNum];
        var numTiles = lData.Rows * lData.Cols;
        var targetPairs = numTiles / 2;
        var selectedLevelImages = new Array();

        ImageManager.ShuffleImages();

        for (int i = 0; i < targetPairs; i++)
        {
            selectedLevelImages.Add(ImageManager.GetImage(i));
            selectedLevelImages.Add(ImageManager.GetImage(i));
        }

        selectedLevelImages.Shuffle();

        return new Dictionary{
            {"target_pairs",targetPairs},
            {"num_cols",lData.Cols},
            {"image_list",selectedLevelImages},
        };
    }

    public void ClearNodesOfGroup(string groupName)
    {
        foreach (var item in GetTree().GetNodesInGroup(groupName))
        {
            item.QueueFree();
        }
    }

}
