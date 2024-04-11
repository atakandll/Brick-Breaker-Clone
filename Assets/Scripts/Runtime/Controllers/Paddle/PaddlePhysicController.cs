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
            if (other.gameObject.CompareTag("Ball"))
            {
                PaddleSignals.Instance.onInteractionWithBall?.Invoke();
                AudioSignals.Instance.onInteractionPaddleSound?.Invoke();
                BounceBallWithPaddle(other);
            }
        }
        private void BounceBallWithPaddle(Collider2D ballCollider)
        {
            var ballRigidbody = ballCollider.attachedRigidbody;
            var transform1 = transform;
            var hitPointDifference = ballCollider.transform.position.x - transform1.position.x;
            var normalizedDifference = hitPointDifference / (transform1.localScale.x / 2);
            var maxBounceAngleRadians = maxBounceAngle * Mathf.Deg2Rad;
            var newDirection = new Vector2(Mathf.Sin(maxBounceAngleRadians * normalizedDifference), 1).normalized;

            ballRigidbody.velocity = newDirection * ballRigidbody.velocity.magnitude;
        }
    }
}