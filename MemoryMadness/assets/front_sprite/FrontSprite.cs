using System;
using Godot;

public partial class FrontSprite : TextureRect
{

    public Vector2 SCALE_SMALL = new Vector2(0.1f, 0.1f);
    public Vector2 SCALE_NORMAL = new Vector2(1.0f, 1.0f);
    public Vector2 SPIN_TIME_RANGE = new Vector2(1.0f, 2.0f);
    public float SCALE_TIME = 0.5f;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        RunMe();
    }

    public float GetRandomSpinTime()
    {
        return (float)GD.RandRange(SPIN_TIME_RANGE.X, SPIN_TIME_RANGE.Y);
    }

    public float GetRandomRotation()
    {
        return Mathf.DegToRad(GD.RandRange(-360, 360));
    }

    public void RunMe()
    {
        var tween = CreateTween();
        tween.SetLoops();
        tween.TweenProperty(this, "scale", SCALE_NORMAL, SCALE_TIME);
        tween.TweenProperty(this, "rotation", GetRandomRotation(), GetRandomSpinTime());
        tween.TweenProperty(this, "rotation", GetRandomRotation(), GetRandomSpinTime());
        tween.TweenProperty(this, "scale", SCALE_SMALL, SCALE_TIME);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
