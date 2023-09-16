using System;

namespace TappyPlane.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class OnReadyAttribute : Attribute
    {
        public string NodePath { get; }

        public OnReadyAttribute(string? NodePath = "")
        {
            NodePath = NodePath;
        }

    }
}
