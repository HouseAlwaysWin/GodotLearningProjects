using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class Cup : StaticBody2D
{
    [OnReady]
    public AnimationPlayer AnimationPlayer;

    [OnReady]
    public AudioStreamPlayer2D VanishSound;

    [OnReady("/root/GameManager")]
    public GameManager GameManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        VanishSound.Finished += OnVanishSoundFinished;
    }

    private void OnVanishSoundFinished()
    {
        this.GameManager.EmitSignal(GameManager.SignalName.OnCupDestroyed);
        QueueFree();
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void Die()
    {
        VanishSound.Play();
        AnimationPlayer.Play("vanish");
    }
}
