using System.Collections.Generic;

namespace Jackoo.Utils
{
    public static class ListUtil
    {
        public static T Random<T>(this IList<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static T Random<T>(this IList<T> list, int min, int max)
        {
            min = min < 0 ? 0 : min;
            max = max >= list.Count ? list.Count : max;
            return list[UnityEngine.Random.Range(min, max)];
        }
    }
}