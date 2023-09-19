using Godot;

public partial class GameManager : Node
{
	public PackedScene GameScene = GD.Load<PackedScene>("res://Game/Game.tscn");
	public void LoadGameScene()
	{
		GetTree().ChangeSceneToPacked(GameScene);
	}
}
