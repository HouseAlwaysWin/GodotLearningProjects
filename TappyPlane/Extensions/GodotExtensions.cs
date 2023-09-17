using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Godot;

namespace TappyPlane.Extensions.Attributes
{
    public static class GodotExtensions
    {
        public static void InitOnReady<T>(this T node) where T : Node
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
            // var fields = typeof(T).GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

            // foreach (var field in fields)
            // {
            //     var attributes = field.GetCustomAttributes(true);

            //     foreach (var attribute in attributes)
            //     {
            //         if (attribute is OnReadyAttribute onReadyAttribute)
            //         {
            //             var nodePath = onReadyAttribute.NodePath;
            //             if (string.IsNullOrWhiteSpace(nodePath))
            //             {
            //                 nodePath = field.Name;
            //             }
            //             field.SetValue(node, node.GetNode(nodePath));
            //         }
            //     }
            // }
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



        public static Vector2 SetXPosition(this Vector2 vector2, float x = 0)
        {
            return new Vector2(x, vector2.Y);
        }

        public static Vector2 SetYPosition(this Vector2 vector2, float y = 0)
        {
            return new Vector2(vector2.X, y);
        }
    }
}
