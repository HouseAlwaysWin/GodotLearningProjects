using Godot;
using System;

public partial class Level : Node2D
{
    public Area2D DeathZone;
    public Start Start;

    public Player Player;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        DeathZone = GetNode<Area2D>("DeathZone");
        DeathZone.BodyEntered += DeathZoneBodyEntered;

        Start = GetNode<Start>("Start");
        Player = GetTree().GetFirstNodeInGroup("player") as Player;
        Player.GlobalPosition = Start.GetSpawnPos();
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
        Player.GlobalPosition = Start.GetSpawnPos();
    }
}
