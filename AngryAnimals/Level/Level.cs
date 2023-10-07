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


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        this.GameManager.Connect(GameManager.SignalName.OnUpdateDebugLabel, Callable.From<string>(OnUpdateDebugLabel));
    }

    private void OnUpdateDebugLabel(string text)
    {
        this.DebugLabel.Text = text;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
