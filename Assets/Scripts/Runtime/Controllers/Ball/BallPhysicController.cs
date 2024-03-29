using System;
using Runtime.Controllers.Paddle;
using Runtime.Extensions.ObjectPooling;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.Serialization;
using Logger = Runtime.Extensions.Logger;

namespace Runtime.Controllers.Ball
{
    public class BallPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private BallManager ballManager;

        #endregion

        #endregion

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PaddlePhysicController paddlePhysicController))
            {
               ballManager.OnInteractionPaddle();
            }
            if (other.gameObject.CompareTag("Brick"))
            {

            }
            if (other.gameObject.CompareTag("Edge"))
            {
                ballManager.OnInteractionEdge();
                
            }
        }
    }
}