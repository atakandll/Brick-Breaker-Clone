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

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_paddle))
            {
                BallSignals.Instance.onInteractionPaddle?.Invoke(other.gameObject);
                Logger.Instance.Log<BallPhysicController>("OnCollisionPaddle", "green");
            }
            else if (other.gameObject.CompareTag(_brick))
            {
                BallSignals.Instance.onInteractionBrick?.Invoke(other.gameObject);
                Logger.Instance.Log<BallPhysicController>("OnCollisionBrick", "green");
            }
            else if (other.gameObject.CompareTag(edge))
            {
                BallSignals.Instance.onInteractionEdge?.Invoke(other.gameObject);
                Logger.Instance.Log<BallPhysicController>("OnCollisionEdge", "green");
                
            }
        }
    }
}