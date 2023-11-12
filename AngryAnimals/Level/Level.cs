using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class Level : Node2D
{
    [OnReady("DebugLabel")]
    public Label DebugLabel;

    [OnReady("/root/GameManager")]
    public GameManager GameManager;

    [OnReady("AnimalStart")]
    public Marker2D AnimalStart;

    public PackedScene AnimalScene;




    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AnimalScene = GD.Load<PackedScene>("res://Animal/animal.tscn");
        this.InitOnReady();
        Setup();
        this.GameManager.Connect(GameManager.SignalName.OnUpdateDebugLabel, Callable.From<string>(OnUpdateDebugLabel));
        this.GameManager.Connect(GameManager.SignalName.OnAnimailDied, Callable.From(OnAnimalDied));
        OnAnimalDied();
    }

    private void OnAnimalDied()
    {
        Animal animal = (Animal)this.AnimalScene.Instantiate();
        animal.GlobalPosition = AnimalStart.GlobalPosition;
        AddChild(animal);
    }

    private void OnUpdateDebugLabel(string text)
    {
        this.DebugLabel.Text = text;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsKeyPressed(Key.Q))
        {
            GameManager.LoadMainScene();
        }
    }

    public void Setup()
    {
        var tc = GetTree().GetNodesInGroup(this.GameManager.GROUP_CUP);
        this.GameManager.SetTargetCups(tc.Count);
    }
}
