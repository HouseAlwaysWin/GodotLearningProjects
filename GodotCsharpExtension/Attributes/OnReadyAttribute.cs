using System;

namespace GodotCsharpExtension.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class OnReadyAttribute : Attribute
    {
        public string NodePath { get; }
        public bool IsAutoLoad { get; }

        public OnReadyAttribute(string? nodePath = "", bool isAutoLoad = false)
        {
            // if (isAutoLoad)
            // {
            //     nodePath = $"/root/{nodePath}";
            // }
            IsAutoLoad = isAutoLoad;
            NodePath = nodePath;
        }

    }
}
