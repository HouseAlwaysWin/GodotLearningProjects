using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class GameUI : CanvasLayer
{

    [OnReady("/root/GameManager")]
    public GameManager GameManager;

    [OnReady("MC/VB1/LevelLabel")]
    public Label LevelLabel;

    [OnReady("MC/VB1/AttemptsLabel")]
    public Label AttemptsLabel;

    [OnReady("MC/VB2")]
    public VBoxContainer VB2;

    [OnReady]
    public AudioStreamPlayer2D Sounds;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        this.LevelLabel.Text = $"Level {this.GameManager.GetLevelSelected()}";
        this.GameManager.Connect(GameManager.SignalName.OnAttemptMade, Callable.From(OnAttemptMade));
        this.GameManager.Connect(GameManager.SignalName.OnGameOver, Callable.From(OnGameOver));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (this.VB2.Visible && Input.IsKeyPressed(Key.Space))
        {
            this.GameManager.LoadMainScene();
        }
    }

    public void OnAttemptMade()
    {
        this.AttemptsLabel.Text = $"Attempts {this.GameManager.GetAttempts()}";
    }

    public void OnGameOver()
    {
        this.VB2.Show();
        this.Sounds.Play();
    }
}
