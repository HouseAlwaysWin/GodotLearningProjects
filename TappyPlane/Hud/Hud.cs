using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class Hud : Control
{
    [OnReady("MarginContainer/ScoreLabel")]
    public Label ScoreLabel;

    [OnReady("/root/GameManager")]
    public GameManager GameManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        this.GameManager.Connect(GameManager.SignalName.OnSroceUpdated, Callable.From(OnScoreUpdated));
    }

    private void OnScoreUpdated()
    {
        ScoreLabel.Text = this.GameManager.Score.ToString();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
