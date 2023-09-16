using System.Runtime.CompilerServices;
using System.Globalization;
using Godot;
using System;
using TappyPlane.Extensions.Attributes;

public partial class PlaneCB : CharacterBody2D
{
	const float GRAVITY = 300f;
	const float POWER = -400f;

	[OnReady]
	AnimationPlayer animationPlayer;

	[OnReady]
	AnimatedSprite2D animatedSprite2D;

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
			animationPlayer.Play("fly");
		}
	}

	public void Die()
	{
		animatedSprite2D.Stop();
		SetProcess(false);
	}

}
