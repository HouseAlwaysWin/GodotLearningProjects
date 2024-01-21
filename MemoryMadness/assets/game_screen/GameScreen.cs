using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class GameScreen : Control
{

    [OnReady("HB/MC2/VBoxContainer/ExitButton")]
    public TextureButton ExitButton;

    [OnReady("/root/SignalManager")]
    public SignalManager SGManager;

    [OnReady("/root/SoundManager")]
    public SoundManager SoundManager;



    [OnReady("Sound")]
    public AudioStreamPlayer Sound;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        ExitButton.Pressed += OnExitButtonPressed;
    }

    private void OnExitButtonPressed()
    {
        SoundManager.PlayButtonClick(Sound);
        SGManager.EmitSignal(SignalManager.SignalName.OnGameExitPressed);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
