using System;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
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
        [SerializeField] private new Rigidbody2D managerRigidbody;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private float maxBounceAngle = 50f;

        #endregion

        #region Private Variables

        private Vector2 _previousSize;

        #endregion

        #endregion

        private void Awake() => GetPositions();

        private void GetPositions() => _previousSize = boxCollider.size;

        private void Update()
        {
            if (transform.localScale.x != _previousSize.x || transform.localScale.y != _previousSize.y)
                UpdateColliderSize();

            var transform1 = transform;
            var localScale = transform1.localScale;
            _previousSize = new Vector2(localScale.x, localScale.y);
        }

        void UpdateColliderSize()
        {
            var localScale = transform.localScale;
            boxCollider.size = new Vector2(localScale.x + 2, localScale.y + 2);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Paddle"))
            {
                Debug.Log("Paddle la etkileşime girdi");
                ShakeSignals.Instance.onPaddleShake?.Invoke();
                ballManager.OnInteractionPaddle();
                return;
            }

            if (other.gameObject.CompareTag("Brick"))
            {
                Debug.Log("Brick la etkileşime girdi");
                ReflectBall(other);
                ballManager.OnInteractionWithBricks();
                PushToPool(PoolObjectType.Bricks, other.gameObject);
                return;

            }

            if (other.gameObject.CompareTag("Edge"))
            {
                Debug.Log("Edgele etkileşime girdi");
                ballManager.OnInteractionEdge();

            }
        }

        public void PushToPool(PoolObjectType poolObjectType, GameObject obj)
        {
            PoolSignals.Instance.onReleasePoolObject(poolObjectType, obj);
            Debug.Log("Brickler poola geri döndü");
        }

        private void ReflectBall(Collider2D brick)
        {
            Vector2 directionToBrick = brick.transform.position - transform.position;

            var reflectionDirection = Vector2.Reflect(managerRigidbody.velocity.normalized, directionToBrick.normalized);

            managerRigidbody.velocity = reflectionDirection * managerRigidbody.velocity.magnitude;
        }
    }
}