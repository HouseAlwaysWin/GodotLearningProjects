using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class LevelBase : Node2D
{
    [OnReady]
    public CharacterBody2D Player;

    [OnReady]
    public Camera2D PlayerCam;

    public override void _Ready()
    {
        this.InitOnReady();
    }

    public override void _PhysicsProcess(double delta)
    {
        PlayerCam.Position = Player.Position;
    }
}
