using System.Runtime.CompilerServices;
using System.Globalization;
using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class GameOver : Control
{
    [OnReady]
    public Label GameOverLabel;

    [OnReady]
    public Label PressSpaceLabel;

    [OnReady]
    public Timer Timer;

    [OnReady("/root/GameManager")]
    public GameManager GameManager;

    private bool _canPressSpace = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        Timer.Timeout += OnTimerTimeout;
        // this.GameManager.OnGameOver += OnGameOver;
        this.GameManager.Connect(GameManager.SignalName.OnGameOver, Callable.From(OnGameOver));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (_canPressSpace)
        {
            if (Input.IsActionJustPressed("fly"))
            {
                this.GameManager.LoadMainScene();
            }
        }
    }

    public void OnGameOver()
    {
        Show();
        this.Timer.Start();
    }

    public void RunSequence()
    {
        GameOverLabel.Hide();
        PressSpaceLabel.Show();
        _canPressSpace = true;
    }

    public void OnTimerTimeout()
    {
        RunSequence();
    }
}
