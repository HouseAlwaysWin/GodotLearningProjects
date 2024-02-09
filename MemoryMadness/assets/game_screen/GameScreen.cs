using Godot;
using Godot.Collections;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;

public partial class GameScreen : Control
{

    [OnReady("HB/MC2/VBoxContainer/ExitButton")]
    public TextureButton ExitButton;

    [OnReady("HB/MC1/TileContainer")]
    public GridContainer TileContainer;

    [OnReady("/root/SignalManager")]
    public SignalManager SGManager;

    [OnReady("/root/SoundManager")]
    public SoundManager SoundManager;


    [OnReady("/root/GameManager")]
    public GameManager GameManager;

    [OnReady("/root/ImageManager")]
    public ImageManager ImageManager;

    [Export]
    public PackedScene MemTileScene;

    [OnReady("Sound")]
    public AudioStreamPlayer Sound;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        ExitButton.Pressed += OnExitButtonPressed;
        SGManager.Connect(SignalManager.SignalName.OnLevelSelected, Callable.From<int>(OnLevelSelected));

    }

    private void OnExitButtonPressed()
    {
        SoundManager.PlayButtonClick(Sound);
        SGManager.EmitSignal(SignalManager.SignalName.OnGameExitPressed);
    }

    public void AddMemoryTile(Dictionary iiDict, CompressedTexture2D frameImage)
    {
        var newTile = MemTileScene.Instantiate<MemoryTile>();
        TileContainer.AddChild(newTile);
        newTile.Setup(iiDict, frameImage);
    }

    private void OnLevelSelected(int levelNum)
    {
        var levelSelection = this.GameManager.GetLevelSelection(levelNum);
        var frameImage = this.ImageManager.GetRandomFrameImage();

        TileContainer.Columns = (int)levelSelection["num_cols"];

        foreach (var iiDict in ((Array)levelSelection["image_list"]))
        {
            AddMemoryTile((Dictionary)iiDict, frameImage);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
