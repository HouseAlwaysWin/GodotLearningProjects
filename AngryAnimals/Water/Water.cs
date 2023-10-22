using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class Water : Area2D
{
    [OnReady]
    public AudioStreamPlayer2D SplashSound;

    [OnReady("/root/GameManager")]
    public GameManager GameManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        this.BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body.IsInGroup(this.GameManager.GROUP_ANIMAL))
        {
            SplashSound.Play();
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
