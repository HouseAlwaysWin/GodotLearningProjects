using Godot;
using System;
using System.Collections.Generic;

namespace AlienAttack.Extensions
{
    public class Utils
    {
        public static float Randf()
        {
            Random rand = new Random();
            float randomFloat = (float)rand.NextDouble();
            return randomFloat;
        }
    }

    public static class ListExtension
    {
        public static void Sort<T>(this List<T> list, Func<T, T, int> comparer)
        {
            list.Sort(new Sort_Custom<T>(comparer));
        }

        public static T PickRandom<T>(this IList<T> list)
        {
            Random random = new Random();
            int randomIndex = random.Next(list.Count);
            T randomObj = list[randomIndex];
            return randomObj;
        }


    }

    public class Sort_Custom<T> : IComparer<T>
    {
        // private Vector2 _position;
        Func<T, T, int> _comparer;
        public Sort_Custom(Func<T, T, int> comparer)
        {
            _comparer = comparer;
        }
        public int Compare(T a, T b)
        {
            return _comparer(a, b);
        }
    }

}

