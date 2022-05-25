using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Jackoo.Utils
{
    public static class VectorUtil
    {
        #region Random

        public static Vector3 Random(float radius)
        {
            float[] xyz = new float[3] { 0, 0, 0 };

            for (int i = 0; i < xyz.Length; ++i)
                xyz[i] = UnityEngine.Random.Range(-radius, radius);

            return new Vector3(xyz[0], xyz[1], xyz[2]);
        }

        public static Vector3 Random(this Vector3 center, float radius)
        {
            Vector3 result = new Vector3();

            result = center;
            result += Random(radius);

            return result;
        }

        #endregion

        public static Vector3 Middle(this Vector3 origin, Vector3 target)
        {
            return Vector3.Lerp(origin, target, .5f);
        }
    }
}
