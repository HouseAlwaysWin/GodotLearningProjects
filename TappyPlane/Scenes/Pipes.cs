using System;
using System.Globalization;
using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;

public partial class Pipes : Node2D
{
    const float SCROLL_SPEED = 120f;
    [OnReady]
    public VisibleOnScreenNotifier2D VisibleOnScreenNotifier2D;

    [OnReady]
    public Area2D Upper;

    [OnReady]
    public Area2D Lower;

    [OnReady]
    public Area2D Laser;



    [OnReady("/root/GameManager")]
    public GameManager GameManager;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        VisibleOnScreenNotifier2D.ScreenExited += ScreenExited;
        Upper.BodyEntered += OnPipeBodyEntered;
        Lower.BodyEntered += OnPipeBodyEntered;
        Laser.BodyEntered += OnLaserBodyEntered;
    }

    private void OnPipeBodyEntered(Node2D body)
    {
        if (body.IsInGroup(this.GameManager.GROUP_PLANE))
        {
            ((PlaneCB)body).Die();
        }
    }

    private void PlayerScored()
    {
        this.GameManager.IncrementScore();
    }

    private void OnLaserBodyEntered(Node2D body)
    {
        if (body.IsInGroup(this.GameManager.GROUP_PLANE))
        {
            PlayerScored();
        }
    }



    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Position = Position.MinusEqPos(SCROLL_SPEED * (float)delta, 0);
    }

    public void ScreenExited()
    {
        QueueFree();
    }

}
