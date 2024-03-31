using System;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct BricksSpawnData
    {
        public Vector2 BrickSize;
        public int BricksPerRow;
        public Vector2 Spacing;
        public Vector3 OriginPosition;
        public int SpawnLimit;
        public GameObject BrickSpawnZone;
    }
}