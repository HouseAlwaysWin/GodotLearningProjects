using System;
using Godot;
using Godot.Collections;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;

public partial class FruitPickup : Area2D
{
    [OnReady("AnimatedSprite2D")]
    public AnimatedSprite2D anim;

    [OnReady]
    public Timer LifeTimer;

    public const float GRAVITY = 160.0f;
    public const float JUMP = -200.0f;
    public const int POINTS = 2;
    public float _startY;
    public float _speedY = JUMP;

    public override void _Ready()
    {
        base._Ready();
        this.InitOnReady();
        LifeTimer.Timeout += OnLifeTimerTimeout;
        _startY = Position.Y;

        var ln = new Array<StringName>();
        foreach (var an in anim.SpriteFrames.GetAnimationNames())
        {
            ln.Add(an);
        }
        anim.Animation = ln.PickRandom();
    }

    private void OnLifeTimerTimeout()
    {
        KillMe();
    }

    public override void _Process(double delta)
    {
        var posY = Position.Y + (_speedY * (float)delta);
        Position = Position.SetY(posY);
        _speedY += Gravity * (float)delta;

        if (Position.Y > _startY)
        {
            SetProcess(false);
        }
    }

    public void KillMe()
    {
        Hide();
        QueueFree();
    }


}
