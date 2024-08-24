using Godot;
using GodotCsharpExtension.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace GodotCsharpExtension
{
    public static class GodotHelper
    {
        public static void InitOnReady<T>(this T node) where T : Node
        {
            try
            {
                var fieldsWithOnReady = typeof(T)
                    .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(field => field.GetCustomAttributes(typeof(OnReadyAttribute), true).Any());

                foreach (var field in fieldsWithOnReady)
                {
                    var onReadyAttribute = (OnReadyAttribute)field.GetCustomAttribute(typeof(OnReadyAttribute), true);
                    var nodePath = string.IsNullOrWhiteSpace(onReadyAttribute.NodePath) ? field.Name : onReadyAttribute.NodePath;
                    field.SetValue(node, node.GetNode(nodePath));
                }
            }
            catch (Exception ex)
            {
                // GD.Print(ex);
                throw ex;
            }
        }



        public static Vector2 MinusEqPos(this Vector2 vector2, float? x = null, float? y = null)
        {

            var tmpX = x == null ? vector2.X : x;
            var tmpY = y == null ? vector2.Y : y;
            vector2 -= new Vector2(tmpX.Value, tmpY.Value);
            return vector2;
        }


        public static Vector2 PlusEqPos(this Vector2 vector2, float? x = null, float? y = null)
        {

            var tmpX = x == null ? vector2.X : x;
            var tmpY = y == null ? vector2.Y : y;
            vector2 += new Vector2(tmpX.Value, tmpY.Value);
            return vector2;
        }

        public static Vector2 MutipleEqPos(this Vector2 vector2, float? x = null, float? y = null)
        {

            var tmpX = x == null ? vector2.X : x;
            var tmpY = y == null ? vector2.Y : y;
            vector2 *= new Vector2(tmpX.Value, tmpY.Value);
            return vector2;
        }

        public static Vector2 SetX(this Vector2 vector2, float x = 0)
        {
            return new Vector2(x, vector2.Y);
        }

        public static Vector2 SetY(this Vector2 vector2, float y = 0)
        {
            return new Vector2(vector2.X, y);
        }

        public static Vector2 SetPos(this Vector2 vector2, float x, float y)
        {
            return new Vector2(x, y);
        }


        public static void SetPos(this Node2D node2D, float x, float y)
        {
            node2D.Position = new Vector2(x, y);
        }

        public static string ToPositionString(this Vector2 vector2, string precisionFormat = "")
        {
            return $"({vector2.X.ToString(precisionFormat)},{vector2.Y.ToString(precisionFormat)})";
        }
    }
}
