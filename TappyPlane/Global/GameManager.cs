using Godot;

public partial class GameManager : Node
{
	public PackedScene GameScene = GD.Load<PackedScene>("res://Game/game.tscn");
	public PackedScene MainScene = GD.Load<PackedScene>("res://Main/main.tscn");
	public void LoadGameScene()
	{
		GetTree().ChangeSceneToPacked(GameScene);
	}

	public void LoadMainScene()
	{
		GetTree().ChangeSceneToPacked(MainScene);
	}
}
