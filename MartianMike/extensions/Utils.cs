using Godot;
using System;

namespace MartianMike.Extensions
{
    public static class Utils
    {
        public static void SetRegionRectPosition(this Sprite2D sprite2D, Vector2 pos)
        {
            sprite2D.RegionRect = new Rect2(pos, sprite2D.RegionRect.Size);
        }

        public static void SetRegionRectPosition(this Sprite2D sprite2D, float x, float y)
        {
            sprite2D.RegionRect = new Rect2(new Vector2(x, y), sprite2D.RegionRect.Size);
        }
    }
}
