using System.Globalization;
using Godot;
using GodotCsharpExtension;
using GodotCsharpExtension.Attributes;

public partial class Pipes : Node2D
{
	const float SCROLL_SPEED = 50f;
	[OnReady]
	public VisibleOnScreenNotifier2D VisibleOnScreenNotifier2D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.InitOnReady();
		VisibleOnScreenNotifier2D.ScreenExited += ScreenExited;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position = Position.MinusEqPos(SCROLL_SPEED * (float)delta, 0);
	}

	public void ScreenExited()
	{
		QueueFree();
	}

}
