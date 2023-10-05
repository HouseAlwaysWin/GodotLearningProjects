using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using Microsoft.VisualBasic.FileIO;
using System;

public partial class ScrollingBG : ParallaxBackground
{

    private const float SPEED = 120f;
    [OnReady("/root/GameManager")]
    public GameManager GameManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        this.GameManager.Connect(GameManager.SignalName.OnGameOver, Callable.From(OnGameOver));
    }

    private void OnGameOver()
    {
        SetProcess(false);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        ScrollOffset.PlusEqPos(x: -(SPEED * ((float)delta)));
    }
}
