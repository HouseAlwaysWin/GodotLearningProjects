using System.Runtime.CompilerServices;
using System.Globalization;
using Godot;
using System;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;

public partial class PlaneCB : CharacterBody2D
{
    const float GRAVITY = 300f;
    const float POWER = -400f;

    [OnReady]
    public AnimationPlayer AnimationPlayer;

    [OnReady]
    public AnimatedSprite2D AnimatedSprite2D;

    [OnReady("/root/GameManager")]
    public GameManager GameManager;

    // [Signal]
    // public delegate void OnPlaneDiedEventHandler();

    public bool isDead = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.InitOnReady();
    }



    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        Velocity += new Vector2(Velocity.X, GRAVITY * (float)delta);

        Fly();
        MoveAndSlide();

        if (IsOnFloor())
        {
            Die();
        }
    }


    public void Fly()
    {
        if (Input.IsActionJustPressed("fly"))
        {
            Velocity += new Vector2(Velocity.X, POWER);
            this.AnimationPlayer.Play("fly");
        }
    }

    public void Die()
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
        this.AnimatedSprite2D.Stop();
        this.GameManager.EmitSignal(GameManager.SignalName.OnGameOver);
        SetPhysicsProcess(false);
    }

}
