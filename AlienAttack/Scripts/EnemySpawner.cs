using System;
using Godot;

public partial class EnemySpawner : Node
{

    // public Timer SpawnerTimer;

    [Export]
    public PackedScene EnemyScene;

    [Signal]
    public delegate void EnemySpawnedEventHandler(Enemy enemy);

    [Export]
    public PackedScene PathEnemyScene;

    [Signal]
    public delegate void PathEnemySpawnedEventHandler(PathEnemy enemy);


    public Node2D SpawnPositions;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (EnemyScene is null)
        {
            EnemyScene = GD.Load<PackedScene>("res://Scenes/enemy.tscn");
        }

        if (PathEnemyScene is null)
        {
            PathEnemyScene = GD.Load<PackedScene>("res://Scenes/path_enemy.tscn");
        }

        SpawnPositions = GetNode<Node2D>("SpawnPositions");

        // SpawnerTimer = GetNode<Timer>("SpawnerTimer");
        // SpawnerTimer.Timeout += OnSpawnerEnemy;
        // SpawnerTimer.Start();

    }

    private void OnSpawnerEnemy()
    {
        var spawnPositionArray = SpawnPositions.GetChildren();
        var randomSpawnPosition = spawnPositionArray.PickRandom() as Marker2D;

        Enemy enemy = EnemyScene.Instantiate() as Enemy;
        // enemy.GlobalPosition = new Vector2(1350, 300);
        enemy.GlobalPosition = randomSpawnPosition.GlobalPosition;
        EmitSignal(SignalName.EnemySpawned, enemy);
        // AddChild(enemy);
    }

    public void OnPathSpawnerEnemy()
    {
        var pathEnemyInstance = PathEnemyScene.Instantiate() as PathEnemy;
        EmitSignal(SignalName.PathEnemySpawned, pathEnemyInstance);
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
