using System;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct EdgeShakeData
    {
        public Vector3 positionStrength;
        public Vector3 _rotationStrength;
    }
}