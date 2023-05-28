using Godot;
using System;

public partial class GameOverScreen : Control
{
    // Called when the node enters the scene tree for the first time.
    public Label FinalScore;

    public override void _Ready()
    {
        FinalScore = GetNode<Label>("Panel/FinalScore");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void SetScore(int newScore)
    {
        FinalScore.Text = $"SCORE: {newScore}";
    }

    public void OnRetryButtonPress()
    {
        GetTree().ReloadCurrentScene();
        GetTree().Paused = false;
    }
}
