using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class MasterScreen : CanvasLayer
{

    [OnReady]
    public MainScreen MainScreen;

    [OnReady]
    public Control GameScreen;

    [OnReady]
    public AudioStreamPlayer Sound;

    [OnReady("/root/SignalManager")]
    public SignalManager SGManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        OnGameExitPressed();
        this.SGManager.Connect(SignalManager.SignalName.OnGameExitPressed, Callable.From(OnGameExitPressed));
        this.SGManager.Connect(SignalManager.SignalName.OnLevelSelected, Callable.From<int>(OnLevelSelected));
    }

    public void ShowGame(bool s)
    {
        GameScreen.Visible = s;
        MainScreen.Visible = !s;
    }

    private void OnGameExitPressed()
    {
        ShowGame(false);
        SoundManager.PlaySound(Sound, SoundType.SOUND_MAIN_MENU);
    }

    private void OnLevelSelected(int levelNum)
    {
        ShowGame(true);
        SoundManager.PlaySound(Sound, SoundType.SOUND_IN_GAME);
    }



    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
