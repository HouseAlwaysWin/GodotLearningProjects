using Godot;
using MartianMike.Extensions;
using System;

public partial class BG : ParallaxBackground
{
    [Export]
    public CompressedTexture2D BgTexTure;

    [Export]
    public float ScrollSpeed = 15;

    private Sprite2D sprite2D;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite2D = GetNode<Sprite2D>("ParallaxLayer/Sprite2D");
        if (BgTexTure is null)
        {
            BgTexTure = GD.Load<CompressedTexture2D>("res://assets/textures/bg/Blue.png");
        }
        sprite2D.Texture = BgTexTure;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var pos = sprite2D.RegionRect.Position;
        var positionX = pos.X + ScrollSpeed * (float)delta;
        var positionY = pos.Y + ScrollSpeed * (float)delta;
        sprite2D.SetRegionRectPosition(positionX, positionY);

        if (sprite2D.RegionRect.Position > new Vector2(64, 64))
        {
            sprite2D.SetRegionRectPosition(Vector2.Zero);
        }
    }
}
