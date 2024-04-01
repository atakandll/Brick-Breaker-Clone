using System;
using Runtime.Controllers.Paddle;
using Runtime.Enums;
using Runtime.Interfaces;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.Serialization;
using Logger = Runtime.Extensions.Logger;

namespace Runtime.Controllers.Ball
{
    public class BallPhysicController : MonoBehaviour, IPushObject
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
                PushToPool(PoolObjectType.Bricks, other.gameObject);
                

            }
            if (other.gameObject.CompareTag("Edge"))
            {
                ballManager.OnInteractionEdge();
                
            }
        }
        public void PushToPool(PoolObjectType poolObjectType, GameObject obj)
        {
            PoolSignals.Instance.onReleasePoolObject(poolObjectType, obj);
            Debug.Log("Brickler poola geri döndü");
        }
    }
}