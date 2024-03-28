using System;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;
using Logger = Runtime.Extensions.Logger;

namespace Runtime.Controllers.Ball
{
    public class BallPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private BallManager _ballManager;

        #endregion

        #region Private Variables

        private readonly string _paddle = "Paddle";
        private readonly string _brick = "Brick";
        private readonly string edge = "Edge";

        #endregion

        #endregion

        private void Awake()
        {
            Logger.Instance.Log<BallPhysicController>("BallPhysicControllerActivated", "green");
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_paddle))
            {
                Logger.Instance.Log<BallPhysicController>("OnCollisionPaddle", "green");
               _ballManager.OnInteractionPaddle(other.gameObject);
            }
            if (other.gameObject.CompareTag(_brick))
            {
                Logger.Instance.Log<BallPhysicController>("OnCollisionBrick", "green");
                _ballManager.OnInteractionBrick(other.gameObject);
            }
            if (other.gameObject.CompareTag(edge))
            {
                Logger.Instance.Log<BallPhysicController>("OnCollisionEdge", "green");
                _ballManager.OnInteractionEdge(other.gameObject);
                
            }
        }
    }
}