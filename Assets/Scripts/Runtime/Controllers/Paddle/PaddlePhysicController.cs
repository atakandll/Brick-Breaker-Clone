using System;
using UnityEngine;
using Logger = Runtime.Extensions.Logger;

namespace Runtime.Controllers.Paddle
{
    public class PaddlePhysicController : MonoBehaviour
    {
        [SerializeField] private float maxBounceAngle = 75f;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Ball")) 
                return;
            
            Rigidbody2D ball = other.rigidbody;
            Collider2D paddle = other.otherCollider;

            Vector2 ballDirection = ball.velocity.normalized;
            Vector2 contactDistance = paddle.bounds.center - ball.transform.position;

            float bounceAngle = (contactDistance.x / paddle.bounds.size.x) * maxBounceAngle;
            ballDirection = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * ballDirection;

            ball.velocity = ballDirection * ball.velocity.magnitude;
        }
    }
}