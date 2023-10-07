using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;
using System.Collections;
using System.Text;

public partial class Animal : RigidBody2D
{
    [OnReady("/root/GameManager")]
    public GameManager GameManager;

    [OnReady]
    public VisibleOnScreenNotifier2D VisibleOnScreenNotifier2D;

    public bool Dead = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
        this.VisibleOnScreenNotifier2D.ScreenExited += OnScreenExited;
    }

    private void OnScreenExited()
    {
        Died();
    }

    private void Died()
    {
        if (Dead)
        {
            return;
        }
        Dead = true;
        this.GameManager.EmitSignal(GameManager.SignalName.OnAnimailDied);
        QueueFree();
    }

    public override void _PhysicsProcess(double delta)
    {

        this.InitOnReady();
        UpdateDebugLabel();
    }

    private void UpdateDebugLabel()
    {
        StringBuilder strBuilder = new($@"g_pos:{GlobalPosition.ToPositionString("0.0")}");
        strBuilder.Append($"\nang:{AngularVelocity.ToString("0.0")} linear: {LinearVelocity.ToPositionString("0.0")}");
        this.GameManager.EmitSignal(GameManager.SignalName.OnUpdateDebugLabel, strBuilder.ToString());
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
