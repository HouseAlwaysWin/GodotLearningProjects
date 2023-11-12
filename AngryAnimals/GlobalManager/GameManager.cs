using Godot;
using Godot.Collections;
using System;

public partial class GameManager : Node
{
    [Signal]
    public delegate void OnUpdateDebugLabelEventHandler(string text);

    [Signal]
    public delegate void OnAnimailDiedEventHandler();

    [Signal]
    public delegate void OnCupDestroyedEventHandler();

    [Signal]
    public delegate void OnGameOverEventHandler();

    [Signal]
    public delegate void OnAttemptMadeEventHandler();

    public PackedScene MainScene = GD.Load<PackedScene>($"res://Main/main.tscn");

    public void LoadMainScene()
    {
        GetTree().ChangeSceneToPacked(MainScene);
    }

    public string GROUP_CUP = "cup";
    public string GROUP_ANIMAL = "animal";

    public int DEFAULT_SCORE = 1000;
    private Dictionary<int, int> _levelScores = new Dictionary<int, int>();
    private int _levelSelected = 0;
    private int _attempts = 0;
    private int _cupsHit = 0;
    private int _targetCups = 0;

    public void _Ready()
    {
        Connect(SignalName.OnCupDestroyed, Callable.From(() =>
        {
            _cupsHit += 1;
            GD.Print($@"target cups:{_targetCups}, attempts:{_attempts}, cup hit: {_cupsHit}");
            CheckGameOver();
        }));
    }

    public void CheckAndAdd(int level)
    {
        if (!_levelScores.ContainsKey(level))
        {
            _levelScores[level] = DEFAULT_SCORE;
        }
    }

    public void LevelSelected(int level)
    {
        CheckAndAdd(level);
        _levelSelected = level;
        _attempts = 0;
        _cupsHit = 0;
        GD.Print($"Level Selected:{_levelSelected},Level Score:{_levelScores[level]}");
    }

    public int GetBestForLevel(int level)
    {
        CheckAndAdd(level);
        return _levelScores[level];
    }

    public int GetAttempts()
    {
        return _attempts;
    }

    public int GetLevelSelected()
    {
        return _levelSelected;
    }

    public void AttemptMade()
    {
        _attempts += 1;
        EmitSignal(SignalName.OnAttemptMade);
        GD.Print($@"target cups:{_targetCups}, attempts:{_attempts}, cup hit: {_cupsHit}");
    }

    public void CheckGameOver()
    {
        if (_cupsHit >= _targetCups)
        {
            GD.Print($"GAME OVER {_levelScores}");
            EmitSignal(SignalName.OnGameOver);
            if (_levelScores[_levelSelected] > _attempts)
            {
                _levelScores[_levelSelected] = _attempts;
                GD.Print("Record set:", _levelScores);
            }
        }
    }

    public void SetTargetCups(int t)
    {
        _targetCups = t;
        GD.Print($"Set Target Cups:{_targetCups}");
    }
}
