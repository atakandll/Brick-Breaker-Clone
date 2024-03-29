using System;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable] 
    public struct BallSpawnData
    {
        public int SpawnLimit;
        public GameObject BallSpawnZone;
    }
}