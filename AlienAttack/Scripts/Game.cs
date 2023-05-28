using System.Threading.Tasks;
using Godot;
using System;

public partial class Game : Node2D
{

    public int PlayerLives = 3;
    public int Score = 0;

    // public Area2D Deathzone;
    public Player Player;

    public Hud HUD;

    public CanvasLayer UI;

    [Export]
    public PackedScene GameOverScreenScene;

    public AudioStreamPlayer2D EnemyHitSound;
    public AudioStreamPlayer2D PlayerDamageSound;



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Deathzone = GetNode<Area2D>("Deathzone");
        // Deathzone.AreaEntered += OnEnterDeathZone;

        Player = GetNode<Player>("Player");
        Player.ScoreEnemy += OnEnemyDied;
        // Player.TakeDamage += OnTakePlayerDamage;

        UI = GetNode<CanvasLayer>("UI");

        HUD = GetNode<Hud>("UI/HUD");
        HUD.SetScoreLabel(0);
        HUD.SetLives(PlayerLives);

        if (GameOverScreenScene is null)
        {
            GameOverScreenScene = GD.Load<PackedScene>("res://Scenes/game_over_screen.tscn");
        }

        EnemyHitSound = GetNode<AudioStreamPlayer2D>("EnemyHitSound");
        PlayerDamageSound = GetNode<AudioStreamPlayer2D>("PlayerDamageSound");
    }

    public async void OnPlayerTakeDamage()
    {
        PlayerDamageSound.Play();
        PlayerLives -= 1;
        HUD.SetLives(PlayerLives);
        if (PlayerLives == 0)
        {
            Player.Die();
            var gameOverScreenInstance = GameOverScreenScene.Instantiate() as GameOverScreen;
            await ToSignal(GetTree().CreateTimer(1.5), "timeout");
            UI.AddChild(gameOverScreenInstance);
            gameOverScreenInstance.SetScore(Score);
            GetTree().Paused = true;
        }
    }



    private void OnEnterDeathZone(Area2D area)
    {
        ((Enemy)area).Die();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void OnEnemySpawned(Enemy enemy)
    {
        // enemy.EnemyDie += OnEnemyDied;
        AddChild(enemy);
    }

    public void OnPathEnemySpawned(PathEnemy pathEnemy)
    {
        AddChild(pathEnemy);
    }

    private void OnEnemyDied()
    {
        Score += 100;
        HUD.SetScoreLabel(Score);
        EnemyHitSound.Play();
    }

}
