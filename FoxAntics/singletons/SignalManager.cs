using Godot;
using System;

public partial class SignalManager : Node
{
    [Signal]
    public delegate void OnEnemyHitEventHandler(int points, Vector2 enemyPosition);

    [Signal]
    public delegate void OnCreateBulletEventHandler(Vector2 pos, Vector2 dir, float lifeSpan, float speed, ObjectType obType);
    [Signal]
    public delegate void OnCreateObjectEventHandler(Vector2 pos, ObjectType obType);

}
