using Godot;

public partial class GameManager : Node
{
	public PackedScene GameScene = GD.Load<PackedScene>("res://Game/game.tscn");
	public PackedScene MainScene = GD.Load<PackedScene>("res://Main/main.tscn");

	[Signal]
	public delegate void OnGameOverEventHandler();

	public override void _Ready()
	{
	}

	public void OnGameOverEmit()
	{
		EmitSignal(SignalName.OnGameOver);
	}

	public void LoadGameScene()
	{
		GetTree().ChangeSceneToPacked(GameScene);
	}

	public void LoadMainScene()
	{
		GetTree().ChangeSceneToPacked(MainScene);
	}

}
