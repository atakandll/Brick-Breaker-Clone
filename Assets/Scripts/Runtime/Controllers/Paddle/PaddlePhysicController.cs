using System;
using DG.Tweening;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Paddle
{
    public class PaddlePhysicController : MonoBehaviour
    {
        [SerializeField] private float maxBounceAngle = 50f;
        [SerializeField] public PaddleManager paddleManager;
        
      

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ball"))
            {
                PaddleSignals.Instance.onInteractionWithBall?.Invoke();

            }
        }

        
    }
}