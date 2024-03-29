using System;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct BricksSpawnData
    {
        public int SpawnLimit;
        public GameObject BrickSpawnZone;
    }
}