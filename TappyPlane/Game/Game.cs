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
    public Marker2D SpawnU;
    [OnReady]
    public Marker2D SpawnL;
    [OnReady("/root/GameManager")]
    public GameManager GameManager;

    [OnReady]
    public PlaneCB PlaneCB;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        SpawnTimer.Timeout += OnSpawnTimerTimeout;

        // PlaneCB.OnPlaneDied += OnPlaneDied;
        this.GameManager.OnGameOver += OnGameOver;
        SpawnPipes();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void SpawnPipes()
    {
        var yPos = GD.RandRange(SpawnU.Position.Y, SpawnL.Position.Y);
        Node2D newPipes = (Node2D)PipesScene.Instantiate();
        newPipes.SetPosition(SpawnL.Position.X, (float)yPos);
        PipeHolder.AddChild(newPipes);
    }

    public void OnSpawnTimerTimeout()
    {
        SpawnPipes();
    }

    public void OnGameOver()
    {

    }


    // public void OnPlaneDied()
    // {
    //     GameManager.LoadMainScene();
    // }

}
