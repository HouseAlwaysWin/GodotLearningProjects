using Godot;
using System;

public partial class StartMenu : Control
{

    public Button StartButton;
    public Button QuitButton;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StartButton = GetNode<Button>("StartButton");
        QuitButton = GetNode<Button>("QuitButton");

        StartButton.Pressed += StartButtonPressed;
        QuitButton.Pressed += QuitButtonPressed;
    }

    private void QuitButtonPressed()
    {
        GetTree().Quit();
    }

    private void StartButtonPressed()
    {
		GetTree().ChangeSceneToFile("res://scenes/level.tscn");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
