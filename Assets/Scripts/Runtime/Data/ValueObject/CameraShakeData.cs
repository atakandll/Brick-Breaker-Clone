using System;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct CameraShakeData
    {
        public Vector3 positionStrength;
        public Vector3 _rotationStrength;
    }
}