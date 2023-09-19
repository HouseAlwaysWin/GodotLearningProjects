using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;
using System;

public partial class Main : Control
{

	[OnReady("/root/GameManager")]
	public GameManager GameManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.InitOnReady();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("fly"))
		{
			GameManager.LoadGameScene();
		}
	}
}
