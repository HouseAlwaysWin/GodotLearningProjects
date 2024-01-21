using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class LevelButton : TextureButton
{
    [OnReady]
    public Label Label;

    [OnReady]
    public AudioStreamPlayer Sound;

    [OnReady("/root/SignalManager")]
    public SignalManager SGManager;

    private int _levelNumber = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        Pressed += OnPress;
        // this.Label.Text = "3x4";
    }

    public void SetLevelNumber(int levelNum)
    {
        _levelNumber = levelNum;
        var lData = GameManager.LEVELS[_levelNumber];
        Label.Text = $"{lData.Rows}x{lData.Cols}";

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void OnPress()
    {
        SoundManager.PlayButtonClick(Sound);
        this.SGManager.EmitSignal(SignalManager.SignalName.OnLevelSelected, _levelNumber);
    }
}
