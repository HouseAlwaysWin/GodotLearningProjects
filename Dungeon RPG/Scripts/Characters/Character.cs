

using Godot;

public abstract partial class Character : CharacterBody3D
{
    [ExportGroup("Required Node")]
    [Export] public AnimationPlayer AnimationPlayer { get; private set; }
    [Export] public Sprite3D Sprite3D { get; private set; }
    [Export] public StateMachine StateMachineNode { get; private set; }

    [Export] public float Gravity = 100f;

    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode { get; private set; }
    [Export] public NavigationAgent3D AgentNode { get; private set; }
    [Export] public Area3D ChaseAreaNode { get; private set; }
    public Vector2 direction = new();

    public override void _PhysicsProcess(double delta)
    {
        SetupGravity(delta);
    }

    protected void SetupGravity(double delta)
    {
        var velocity = Velocity;
        velocity.Y -= (float)delta * Gravity;
        Velocity = velocity;
        var motion = velocity * (float)delta;
        MoveAndCollide(motion);
    }


    public void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;

        if (isNotMovingHorizontally) return;

        bool isMovingLeft = Velocity.X < 0;
        Sprite3D.FlipH = isMovingLeft;
    }
}