using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class GameOver : Control
{
    [OnReady("NinePatchRect/MC/VB/HB/MovesLabel")]
    public Label MovesLabel;

    [OnReady("NinePatchRect/MC/VB/ExitButton")]
    public TextureButton ExitButton;

    [OnReady("/root/SignalManager")]
    public SignalManager SGManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        ExitButton.Pressed += OnExitButtonPressed;
        SGManager.Connect(SignalManager.SignalName.OnGameOver, Callable.From<int>(OnGameOver));
    }

    private void OnGameOver(int moves)
    {
        MovesLabel.Text = moves.ToString();
        Show();
    }

    private void OnExitButtonPressed()
    {
        Hide();
        this.SGManager.EmitSignal(SignalManager.SignalName.OnGameExitPressed);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
