using Godot;
using System;

public partial class Hud : Control
{
    public Label ScoreLabel;
    public Label LiveLabel;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ScoreLabel = GetNode<Label>("Score");
        LiveLabel = GetNode<Label>("Live");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void SetScoreLabel(int newScore)
    {
        ScoreLabel.Text = $"Score:  {newScore}";
    }

    public void SetLives(int amount)
    {
        LiveLabel.Text = amount.ToString();
    }
}
