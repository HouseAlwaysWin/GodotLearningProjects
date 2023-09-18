using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class Game : Node2D
{

    [Export]
    public PackedScene PipesScene;

    [OnReady]
    public Timer SpawnTimer;
    [OnReady]
    public Node PipeHolder;
    [OnReady]
    public Marker2D SpwanU;
    [OnReady]
    public Marker2D SpawnL;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        SpawnTimer.Timeout += OnSpawnTimerTimeout;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void SpawnPipes()
    {
    }

    public void OnSpawnTimerTimeout()
    {

    }

}
