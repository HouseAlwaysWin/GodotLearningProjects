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

    [OnReady("SignalManager", true)]
    private SignalManager _signalManager;

    public override void _Ready()
    {
        this.InitOnReady();
    }

    public override void _PhysicsProcess(double delta)
    {
        PlayerCam.Position = Player.Position;
        if (Input.IsActionJustPressed("bullet"))
        {
            try
            {
                _signalManager.EmitSignal(SignalManager.SignalName.OnCreateBullet,
                    new Vector2(70, -70),
                    new Vector2(50, -50),
                    3.0f,
                    40.0f,
                    (int)ObjectType.BULLET_PLAYER
                );
            }
            catch (Exception ex)
            {
                GD.Print(ex);
            }
        }
    }
}
