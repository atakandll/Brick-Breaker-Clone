﻿using System;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct PaddleData
    {
        public float SideWaysSpeed;
        public Vector3 positionStrength;
        public Vector3 _rotationStrength;
    }
    
}