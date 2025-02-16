using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.Model
{
    public class Placer
    {
        public Vector3[] GetPositionsFromCubeArea(Vector3 center, Vector3 areaSize, float areaSliceFactor)
        {
            float cubeCenterFactor = 0.5f;

            Vector3 topLeftCorner = center + cubeCenterFactor * areaSize;
            Vector3 bottomRightCorner = center - 0.5f * areaSize;

            Vector3 spawnPivot = new Vector3(topLeftCorner.x - 0.5f * areaSize.x * areaSliceFactor,
                topLeftCorner.y - 0.5f * areaSize.y * areaSliceFactor, topLeftCorner.z - 0.5f * areaSize.z * areaSliceFactor);

            List<Vector3> area = new List<Vector3>();

            for (float y = spawnPivot.y; y > bottomRightCorner.y; y -= areaSize.y * areaSliceFactor)
            {
                for (float x = spawnPivot.x; x > bottomRightCorner.x; x -= areaSize.x * areaSliceFactor)
                {
                    for (float z = spawnPivot.z; z > bottomRightCorner.z; z -= areaSize.z * areaSliceFactor)
                    {
                        area.Add(new Vector3(x, y, z));
                    }
                }
            }

            return area.ToArray();
        }
    }
}
