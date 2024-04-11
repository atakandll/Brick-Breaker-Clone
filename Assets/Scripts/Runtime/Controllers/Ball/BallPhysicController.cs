using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Ball
{
    public class BallPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private BallManager ballManager;
        [SerializeField] private new Rigidbody2D managerRigidbody;
        [SerializeField] private BoxCollider2D boxCollider;

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
                ShakeSignals.Instance.onPaddleShake?.Invoke();
                BallSignals.Instance.onInteractionAllObjects?.Invoke();
                ballManager.OnInteractionPaddle();
            }

            if (other.gameObject.CompareTag("Brick"))
            {
                var brickManager = other.GetComponent<BrickManager>();
                ReflectBall(other);
                ballManager.OnInteractionWithBricks();
                
                if ( brickManager != null)
                {
                    brickManager.DestroyBrick();
                }
            }
            if (other.gameObject.CompareTag("Edge"))
            {
                AudioSignals.Instance.onInteractionEdgeSound?.Invoke();
                ballManager.OnInteractionEdge();
                BallSignals.Instance.onInteractionAllObjects?.Invoke();
            }
        }
        private void ReflectBall(Collider2D brick)
        {
            Vector2 directionToBrick = brick.transform.position - transform.position;
            var reflectionDirection = Vector2.Reflect(managerRigidbody.velocity.normalized, directionToBrick.normalized);
            managerRigidbody.velocity = reflectionDirection * managerRigidbody.velocity.magnitude;
        }
    }
}