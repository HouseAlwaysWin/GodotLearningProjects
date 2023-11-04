using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;
using System.Diagnostics.Contracts;

public partial class LevelButton : TextureButton
{
    public Vector2 HOVER_SCALE = new Vector2(1.1f, 1.1f);
    public Vector2 DEFAULT_SCALE = new Vector2(1.0f, 1.0f);

    [Export]
    public int LevelNumber = 1;

    [OnReady("MC/VB/LevelLabel")]
    public Label LevelLabel;

    [OnReady("MC/VB/ScoreLabel")]
    public Label ScoreLabel;

    public PackedScene _levelScene;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        _levelScene = GD.Load<PackedScene>($"res://Level/level_{this.LevelNumber}.tscn");

        LevelLabel.Text = LevelNumber.ToString();
        Pressed += OnPressed;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
    }

    private void OnMouseExited()
    {
        Scale = DEFAULT_SCALE;
    }

    private void OnMouseEntered()
    {
        Scale = HOVER_SCALE;
    }

    private void OnPressed()
    {
        GetTree().ChangeSceneToPacked(_levelScene);
    }






    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
