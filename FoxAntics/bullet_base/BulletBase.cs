using Godot;
using System;

public partial class BulletBase : Area2D
{

    public Vector2 _direction = new Vector2(100, -50);
    public float _lifeSpan = 5.0f;
    public float _lifeTime = 0.0f;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D area)
    {
        QueueFree();
    }

    public override void _Process(double delta)
    {
        float fDelta = (float)delta;
        Position += _direction * fDelta;
        CheckedExpired(fDelta);
    }

    private void CheckedExpired(float delta)
    {
        _lifeTime += delta;
        if (_lifeTime > _lifeSpan)
        {
            QueueFree();
        }
    }

    public void Setup(Vector2 pos, Vector2 dir, float speed, float ls)
    {
        _direction = dir.Normalized() * speed;
        _lifeSpan = ls;
        GlobalPosition = pos;
    }




}
