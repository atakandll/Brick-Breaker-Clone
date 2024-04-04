using System;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public class BallData
    {
        public float Speed;
        public int MaxSpeed;
        public float ScaleUpSize;
        public float ScaleUpDuration;
        public float ScaleDownDuration;
        public Color OriginalColor;
        public float ColorChangeDuration;

    }
   [Serializable]
    public struct BallSpriteData
    {
      
    }
}