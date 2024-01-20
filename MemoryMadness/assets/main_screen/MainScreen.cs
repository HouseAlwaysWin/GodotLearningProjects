using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class MainScreen : Control
{

    [Export]
    public PackedScene LevelButtonScene;

    [OnReady("VB/HBLevels")]
    public HBoxContainer HbLevels;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        SetupGrid();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void CreateLevelButton(int ln)
    {
        var newLb = LevelButtonScene.Instantiate();
        HbLevels.AddChild(newLb);
        ((LevelButton)newLb).SetLevelNumber(ln);
    }

    public void SetupGrid()
    {
        foreach (var level in GameManager.LEVELS)
        {
            CreateLevelButton(level.Key);
        }
    }
}
