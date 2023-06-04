using Godot;
using System;

public partial class UILayer : CanvasLayer
{

    public WinScreen winScreen;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        winScreen = GetNode<WinScreen>("WinScreen");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void ShowWinScreen(bool flag)
    {
        winScreen.Visible = flag;
    }
}
