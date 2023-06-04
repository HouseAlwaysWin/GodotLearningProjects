using Godot;
using System;

public partial class WinScreen : Control
{

    public Button OkButton;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        OkButton = GetNode<Button>("Button");
        OkButton.Pressed += OnButtonPress;
    }

    private void OnButtonPress()
    {
        GetTree().ChangeSceneToFile("res://scenes/level.tscn");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
