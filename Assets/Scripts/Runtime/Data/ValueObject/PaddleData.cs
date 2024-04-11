using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct PaddleData
    {
        public float SideWaysSpeed;
        public Vector3 positionStrength;
        public Vector3 _rotationStrength;
        public float HappyMouthScale;
        public float SadMouthScale;
        public float HappyMouthScaleDuration;
        public float SadMouthScaleDuration;
    }
    
}