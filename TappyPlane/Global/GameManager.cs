using Godot;

public partial class GameManager : Node
{
    public PackedScene GameScene = GD.Load<PackedScene>("res://Game/game.tscn");
    public PackedScene MainScene = GD.Load<PackedScene>("res://Main/main.tscn");

    public string GROUP_PLANE = "plane";


    [Signal]
    public delegate void OnGameOverEventHandler();

    [Signal]
    public delegate void OnSroceUpdatedEventHandler();

    private int _score = 0;
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            if (_score > _highScore)
            {
                _highScore = _score;
            }
            EmitSignal(SignalName.OnSroceUpdated);
        }
    }
    private int _highScore = 0;
    public int HighScore
    {
        get => _highScore;
        set { _highScore = value; }
    }

    public void IncrementScore()
    {
        Score = _score + 1;
        GD.Print(Score);
    }

    public override void _Ready()
    {
    }

    public void LoadGameScene()
    {
        GetTree().ChangeSceneToPacked(GameScene);
    }

    public void LoadMainScene()
    {
        GetTree().ChangeSceneToPacked(MainScene);
    }

}
