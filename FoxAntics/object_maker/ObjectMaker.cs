using Godot;
using Godot.Collections;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

public partial class ObjectMaker : Node2D
{
    public Dictionary<ObjectType, PackedScene> ObjectScenes = new()
    {
        {ObjectType.BULLET_PLAYER, GD.Load<PackedScene>("res://bullet_base/bullet_player.tscn")},
        {ObjectType.BULLET_ENEMY, GD.Load<PackedScene>("res://bullet_base/bullet_enemy.tscn")}
    };

    [OnReady("SignalManager", isAutoLoad: true)]
    private SignalManager _signalManager;

    public override void _Ready()
    {
        this.InitOnReady();
        // _signalManager.OnCreateBullet += OnCreateBullet;
        _signalManager.Connect(SignalManager.SignalName.OnCreateBullet,
        Callable.From<Vector2, Vector2, float, float, ObjectType>(OnCreateBullet));
    }

    private void OnCreateBullet(Vector2 pos, Vector2 dir, float lifeSpan, float speed, ObjectType obType)
    {
        if (!ObjectScenes.ContainsKey(obType)) return;

        var nb = (BulletBase)ObjectScenes[obType].Instantiate();
        nb.Setup(pos, dir, speed, lifeSpan);
        CallDeferred(Node.MethodName.AddChild, nb);

    }
}
