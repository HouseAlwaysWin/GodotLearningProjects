using System;
using Godot;

namespace TappyPlane.Extensions.Attributes
{
    public static class NodeHelper
    {
        public static void InitOnReady<T>(this T node) where T : Node
        {
            var fields = typeof(T).GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                var attributes = field.GetCustomAttributes(true);

                foreach (var attribute in attributes)
                {
                    if (attribute is OnReadyAttribute onReadyAttribute)
                    {
                        var nodePath = onReadyAttribute.NodePath;
                        if (string.IsNullOrWhiteSpace(nodePath))
                        {
                            nodePath = field.FieldType.Name;
                        }
                        field.SetValue(node, node.GetNode(nodePath));
                    }
                }
            }
        }

    }
}
