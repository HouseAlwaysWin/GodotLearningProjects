using Godot;
using System;

public partial class Level : Node2D
{
    public Area2D DeathZone;
    public Start Start;

    public Player Player;

    public Exited Exited;

    [Export]
    public PackedScene NextScene;


    public HUD UITimeHud;

    public UILayer UILayer;

    public Traps TrapAll;

    public Timer TimerNode;

    [Export]
    public float LevelTime = 5;

    public float TimeLeft;

    public bool Win = false;

    [Export]
    public bool IsFinalLevel = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        DeathZone = GetNode<Area2D>("DeathZone");
        DeathZone.BodyEntered += DeathZoneBodyEntered;



        TrapAll = GetNode<Traps>("Traps");
        if (TrapAll != null)
        {
            foreach (var trap in TrapAll.TrapList)
            {
                ((Trap)trap).TouchedPlayer += OnTrapTouchPlayer;
            }
        }
        // Trap.TouchedPlayer += OnSawTouchedPlayer;
        // Trap.TouchedPlayer += OnSpikeBallTouchedPlayer;

        Start = GetNode<Start>("Start");
        Player = GetTree().GetFirstNodeInGroup("player") as Player;
        Player.GlobalPosition = Start.GetSpawnPos();

        // if (NextScene is null)
        // {
        //     NextScene = GD.Load<PackedScene>("res://scenes/level_2.tscn");
        // }

        Exited = GetNode<Exited>("Exited");
        Exited.BodyEntered += OnExitedEntered;

        UITimeHud = GetNode<HUD>("UILayer/HUD");
        UILayer = GetNode<UILayer>("UILayer");

        TimeLeft = LevelTime;

        TimerNode = new Timer();
        TimerNode.Name = "Level Timer";
        TimerNode.WaitTime = 1;
        TimerNode.Timeout += OnLevelTimerTimeout;
        AddChild(TimerNode);
        UITimeHud.SetTimeLabel(TimeLeft);
        TimerNode.Start();
    }

    private void OnLevelTimerTimeout()
    {
        if (!Win)
        {
            TimeLeft -= 1;
            UITimeHud.SetTimeLabel(TimeLeft);
            GD.Print(TimeLeft);
            if (TimeLeft < 0)
            {
                ResetPlayer();
                TimeLeft = LevelTime;
                UITimeHud.SetTimeLabel(TimeLeft);
            }
        }
    }

    private async void OnExitedEntered(Node2D body)
    {

        if (body is Player)
        {
            if (NextScene != null || IsFinalLevel)
            {
                Exited.Animate();
                Player.IsActivated = false;
                await ToSignal(GetTree().CreateTimer(1.3f), SceneTreeTimer.SignalName.Timeout);
                if (IsFinalLevel)
                {
                    UILayer.ShowWinScreen(true);
                }
                else
                {
                    GetTree().ChangeSceneToPacked(NextScene);
                }
            }
        }
    }



    private void DeathZoneBodyEntered(Node2D body)
    {
        ResetPlayer();
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("quit"))
        {
            GetTree().Quit();
        }
        else if (Input.IsActionJustPressed("reset"))
        {
            GetTree().ReloadCurrentScene();
        }
    }

    public void OnTrapTouchPlayer()
    {
        ResetPlayer();
    }

    // public void OnSawTouchedPlayer()
    // {
    //     ResetPlayer();
    // }

    // public void OnSpikeBallTouchedPlayer()
    // {
    //     ResetPlayer();
    // }

    public void ResetPlayer()
    {
        Player.Velocity = Vector2.Zero;
        Player.GlobalPosition = Start.GetSpawnPos();
    }
}
