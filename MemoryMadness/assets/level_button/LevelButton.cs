using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class LevelButton : TextureButton
{
    [OnReady]
    public Label Label;

    [OnReady]
    public AudioStreamPlayer2D Sound;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        this.Label.Text = "3x4";
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
