using System;
using DG.Tweening;
using Runtime.Controllers.Ball;
using Runtime.Data.ValueObject;
using Runtime.Interfaces;
using Runtime.Signals;
using UnityEngine;
using Logger = Runtime.Extensions.Logger;

namespace Runtime.Controllers.Paddle
{
    public class PaddlePhysicController : MonoBehaviour
    {
        [SerializeField] private float maxBounceAngle = 50f;
        
      

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BallPhysicController ballPhysicController))
            {
                //ShakeSignals.Instance.onPaddleShake?.Invoke();
                
            }
        }

        
    }
}