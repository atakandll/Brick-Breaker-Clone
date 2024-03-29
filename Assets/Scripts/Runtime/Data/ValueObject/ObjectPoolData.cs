using System;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct ObjectPoolData
    {
        public PoolObjectType PoolObjectType;
        public PoolType PoolType;
        public GameObject PoolObject;
        public int PoolSize;
    }
}