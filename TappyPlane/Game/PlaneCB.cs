using Godot;
using System;

public partial class PlaneCB : CharacterBody2D
{
	const float GRAVITY = 300f;
	const float POWER = -400f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Velocity += new Vector2(Velocity.X, GRAVITY * (float)delta);

		if (Input.IsActionJustPressed("fly"))
		{
			Velocity += new Vector2(Velocity.X, POWER);
		}

		MoveAndSlide();
	}
}
