using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;




public partial class Player : CharacterBody2D
{
    [OnReady]
    public Sprite2D Sprite2D;

    [OnReady]
    public CollisionShape2D CollisionShape2D;

    [OnReady]
    public AnimationPlayer AnimationPlayer;

    [OnReady]
    public Label DebugLabel;

    [OnReady]
    public AudioStreamPlayer2D SoundPlayer;

    [OnReady(isAutoLoad: true)]
    public SoundManager SoundManager;

    [OnReady]
    public Shooter Shooter;

    private const float GRAVITY = 300f;
    private const float RUN_SPEED = 100f;
    private const float JUMP_VELOCITY = -200f;
    private const float MAX_FALL = 400f;
    private const float HURT_TIME = 0.3f;


    public PLAYER_STATE state = PLAYER_STATE.IDLE;

    public override void _Ready()
    {
        this.InitOnReady();
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = HandleGravity(Velocity, delta);
        GetInput();
        MoveAndSlide();
        CalculateStates();
        UpdateDebugLabel();

        if (Input.IsActionJustPressed("shoot"))
        {
            Shoot();
        }
    }

    private Vector2 HandleGravity(Vector2 velocity, double delta)
    {
        if (!IsOnFloor()) velocity.Y += GRAVITY * (float)delta;
        return velocity;
    }

    public void UpdateDebugLabel()
    {
        DebugLabel.Text = $@"
            floor:{IsOnFloor()} 
            {state}
            ({Velocity.X},{Velocity.Y})
        ";
    }

    public void GetInput()
    {
        Velocity = new Vector2(0, Velocity.Y);
        if (Input.IsActionPressed("jump") && IsOnFloor())
        {
            // Velocity = new Vector2(Velocity.X, JUMP_VELOCITY);
            Velocity = Velocity.SetY(JUMP_VELOCITY);
            SoundManager.PlayClip(SoundPlayer, SoundManager.SOUND_JUMP);
        }

        if (Input.IsActionPressed("left"))
        {
            // Velocity = new Vector2(-RUN_SPEED, Velocity.Y);
            Velocity = Velocity.SetX(-RUN_SPEED);
            Sprite2D.FlipH = true;
        }
        if (Input.IsActionPressed("right"))
        {
            // Velocity = new Vector2(RUN_SPEED, Velocity.Y);
            Velocity = Velocity.SetX(RUN_SPEED);
            Sprite2D.FlipH = false;
        }

        var y = Mathf.Clamp(Velocity.Y, JUMP_VELOCITY, MAX_FALL);
        // Velocity = new Vector2(Velocity.X, y);
        Velocity = Velocity.SetY(y);

    }

    public void CalculateStates()
    {

        if (state == PLAYER_STATE.HURT) return;

        if (IsOnFloor())
        {
            if (Velocity.X == 0)
            {
                SetState(PLAYER_STATE.IDLE);
            }
            else
            {
                SetState(PLAYER_STATE.RUN);
            }
        }
        else
        {
            if (Velocity.Y > 0)
            {
                SetState(PLAYER_STATE.JUMP);
            }
            else
            {
                SetState(PLAYER_STATE.FALL);
            }

        }

    }

    public void SetState(PLAYER_STATE newState)
    {
        if (newState == state) return;

        if (state == PLAYER_STATE.FALL)
        {
            if (newState == PLAYER_STATE.IDLE || newState == PLAYER_STATE.RUN)
            {
                SoundManager.PlayClip(SoundPlayer, SoundManager.SOUND_LAND);
            }
        }

        state = newState;

        switch (state)
        {
            case PLAYER_STATE.RUN:
                AnimationPlayer.Play("run");
                break;
            case PLAYER_STATE.FALL:
                AnimationPlayer.Play("fall");
                break;
            case PLAYER_STATE.HURT:
                AnimationPlayer.Play("hurt");
                break;
            case PLAYER_STATE.JUMP:
                AnimationPlayer.Play("jump");
                break;
            case PLAYER_STATE.IDLE:
                AnimationPlayer.Play("idle");
                break;
            default:
                AnimationPlayer.Play("idle");
                break;
        }
    }


    public void Shoot()
    {
        if (Sprite2D.FlipH)
        {
            Shooter.Shoot(Vector2.Left);
        }
        else
        {
            Shooter.Shoot(Vector2.Right);
        }
    }

}
