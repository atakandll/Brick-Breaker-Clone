using System;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct SpawnData
    {
        public BricksSpawnData BricksSpawnData;
        public BallSpawnData BallSpawnData;
    }
}