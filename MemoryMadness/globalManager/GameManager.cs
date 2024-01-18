using System.Collections.Generic;
using Godot;

public class LevelPos
{
    public int Rows { get; set; }
    public int Cols { get; set; }
}

public partial class GameManager : Node
{
    public Dictionary<int, LevelPos> LEVELS = new()
    {
        { 1 , new LevelPos{ Rows = 2,Cols = 2}},
        { 2 , new LevelPos{ Rows = 3,Cols = 4}},
        { 3 , new LevelPos{ Rows = 4,Cols = 4}},
        { 4 , new LevelPos{ Rows = 4,Cols = 6}},
        { 5 , new LevelPos{ Rows = 5,Cols = 6}},
        { 6 , new LevelPos{ Rows = 6,Cols = 6}},
    };

}
