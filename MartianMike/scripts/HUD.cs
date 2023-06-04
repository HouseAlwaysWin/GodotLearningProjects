using Godot;
using System;

public partial class HUD : Control
{

    public Label TimeLabel;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        TimeLabel = GetNode<Label>("TimeLabel");
    }

    public void SetTimeLabel(float value)
    {
        TimeLabel.Text = $"TIME: {value}";
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
