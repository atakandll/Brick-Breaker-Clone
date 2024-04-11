﻿using Runtime.Data.ValueObject;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Controllers.Ball
{

    public class BallMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody2D rigidbody;

        #endregion

        #region Private Variables

        [Header("Data")] [ShowInInspector] private BallData _data;
        [ShowInInspector] public bool _isReadyToPlay { get; set; }
        [ShowInInspector] private bool isLaunched = false;

        #endregion

        #endregion
        
        public void SetMovementData(BallData data) => _data = data;
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents() => BallSignals.Instance.onPlayConditionChanged += OnPlayConditionChanged;
        private void OnPlayConditionChanged(bool condition) => _isReadyToPlay = condition;

        private void FixedUpdate()
        {
            FaceVelocity();
            if (_isReadyToPlay)
            {
                rigidbody.gravityScale = 1;
                if (!isLaunched)
                {
                    LaunchBall();
                    isLaunched = true;
                }
                rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, _data.MaxSpeed);
            }
            else
            {
                rigidbody.gravityScale = 0;
                Stop();
                isLaunched = false;
            }
        }
        public void LaunchBall()
        {
            Vector2 force = new Vector2(Random.Range(-2f, 2f), 1);
            rigidbody.AddForce(force.normalized * _data.Speed, ForceMode2D.Impulse);
        }
        private void FaceVelocity()
        {
            Vector2 velocity = rigidbody.velocity;

            Vector3 targetPosition = (Vector3)transform.position + new Vector3(velocity.x, velocity.y, 0);

            transform.up = targetPosition - transform.position;
        }
        
        
        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
        }
        private void OnDisable() => UnsubscribeEvents();
        private void UnsubscribeEvents() => BallSignals.Instance.onPlayConditionChanged -= OnPlayConditionChanged;

        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
        }
    }
}