using Godot;
using Godot.Collections;

public partial class Traps : Node2D
{
    public Array<Node> TrapList;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        TrapList = GetTree().GetNodesInGroup("trapItem");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
