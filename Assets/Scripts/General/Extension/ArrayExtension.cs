using System;
using System.Linq;
using System.Collections.Generic;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 2.0.0
namespace General.Extension
{
    public static class ArrayExtension
    {
        public static T Random<T>(this IEnumerable<T> array)
        {
            var enumerable = array as T[] ?? array.ToArray();
            return enumerable.Skip(UnityEngine.Random.Range(0, enumerable.Length)).First();
        }

        public static T Random<T>(this IReadOnlyCollection<T> array) =>
            array.Skip(UnityEngine.Random.Range(0, array.Count)).First();

        public static void ShuffleArray<T>(this IList<T> array)
        {
            var random = new Random();

            for (var i = array.Count - 1; i >= 1; i--)
            {
                var j = random.Next(i + 1);
                (array[j], array[i]) = (array[i], array[j]);
            }
        }
    }
}
