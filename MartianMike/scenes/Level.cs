using Godot;
using System;

public partial class Level : Node2D
{
    public Area2D DeathZone;
    public Marker2D StartPosition;

    public Player Player;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        DeathZone = GetNode<Area2D>("DeathZone");
        DeathZone.BodyEntered += DeathZoneBodyEntered;

        StartPosition = GetNode<Marker2D>("StartPosition");
        Player = GetNode<Player>("Player");
    }

    private void DeathZoneBodyEntered(Node2D body)
    {
        ResetPlayer();
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("quit"))
        {
            GetTree().Quit();
        }
        else if (Input.IsActionJustPressed("reset"))
        {
            GetTree().ReloadCurrentScene();
        }
    }

    public void OnSawTouchedPlayer()
    {
        ResetPlayer();
    }

    public void OnSpikeBallTouchedPlayer()
    {
        ResetPlayer();
    }

    public void ResetPlayer()
    {
        Player.Velocity = Vector2.Zero;
        Player.GlobalPosition = StartPosition.GlobalPosition;
    }
}
