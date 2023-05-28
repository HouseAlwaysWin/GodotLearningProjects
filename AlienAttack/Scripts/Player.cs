using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    public float Speed = 500f;

    [Export]
    public PackedScene RocketScene;

    public Node RocketContainer;

    public AudioStreamPlayer2D RocketShotSound;

    [Signal]
    public delegate void TakeDamageEventHandler();

    [Signal]
    public delegate void ScoreEnemyEventHandler();


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (RocketScene is null)
        {
            RocketScene = GD.Load<PackedScene>("res://Scenes/rocket.tscn");
        }

        RocketContainer = GetNode<Node>("RocketContainer");
        RocketShotSound = GetNode<AudioStreamPlayer2D>("RocketShotSound");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("shoot"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var rocketInstance = RocketScene.Instantiate() as Rocket;
        RocketContainer.AddChild(rocketInstance);

        rocketInstance.GlobalPosition = new Vector2(GlobalPosition.X + 80, GlobalPosition.Y);
        rocketInstance.ScoreEnemy += PlayerScoreEnemy;
        RocketShotSound.Play();
    }

    private void PlayerScoreEnemy()
    {
        EmitSignal(SignalName.ScoreEnemy);
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = new Vector2(0, 0);
        if (Input.IsActionPressed("move_up"))
        {
            Velocity = new Vector2(0, -Speed);
        }
        else if (Input.IsActionPressed("move_down"))
        {
            Velocity = new Vector2(0, Speed);
        }
        else if (Input.IsActionPressed("move_left"))
        {
            Velocity = new Vector2(-Speed, 0);
        }
        else if (Input.IsActionPressed("move_right"))
        {
            Velocity = new Vector2(Speed, 0);
        }
        if (Input.IsActionPressed("move_up") && Input.IsActionPressed("move_left"))
        {
            Velocity = new Vector2(-Speed, -Speed);
        }
        if (Input.IsActionPressed("move_up") && Input.IsActionPressed("move_right"))
        {
            Velocity = new Vector2(Speed, -Speed);
        }
        if (Input.IsActionPressed("move_down") && Input.IsActionPressed("move_left"))
        {
            Velocity = new Vector2(-Speed, Speed);
        }
        if (Input.IsActionPressed("move_down") && Input.IsActionPressed("move_right"))
        {
            Velocity = new Vector2(Speed, Speed);
        }

        // if (GlobalPosition.X < 0)
        // {
        //     GlobalPosition = new Vector2(0, GlobalPosition.Y);
        // }
        // if (GlobalPosition.X > 1280)
        // {
        //     GlobalPosition = new Vector2(1280, GlobalPosition.Y);
        // }

        // if (GlobalPosition.Y < 0)
        // {
        //     GlobalPosition = new Vector2(GlobalPosition.X, 0);
        // }
        // if (GlobalPosition.Y > 720)
        // {
        //     GlobalPosition = new Vector2(GlobalPosition.X, 720);
        // }

        var screenSize = GetViewportRect().Size;
        GlobalPosition = GlobalPosition.Clamp(new Vector2(0, 0), screenSize);

        MoveAndSlide();

    }

    public void TakePlayerDamage()
    {
        EmitSignal(SignalName.TakeDamage);
    }

    public void Die()
    {
        QueueFree();
    }
}
