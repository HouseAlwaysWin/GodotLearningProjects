using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class ScrollingLayer : ParallaxLayer
{

    [OnReady]
    public Sprite2D Sprite2D;

    [Export]
    public Texture2D Texture;

    [Export]
    public float ScrollScale = 0f;

    [Export]
    public float TxX = 1920;
    [Export]
    public float TxY = 1080;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        MotionScale.SetX(ScrollScale);
        var scaleF = GetViewportRect().Size.Y / TxY;
        this.Sprite2D.Texture = Texture;
        this.Sprite2D.Scale = new Vector2(scaleF, scaleF);
        MotionMirroring.SetX(TxX * scaleF);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
