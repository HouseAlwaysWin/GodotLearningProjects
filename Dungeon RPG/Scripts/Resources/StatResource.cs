
using Godot;

[GlobalClass]
public partial class StatResource : Resource
{
    [Export] public Stat StatType { get; private set; }
    [Export] public float StatValue { get; private set; }
}