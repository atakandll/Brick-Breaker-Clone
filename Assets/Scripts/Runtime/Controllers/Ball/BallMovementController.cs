using System;
using System.Security.Cryptography;
using Runtime.Data.ValueObject;
using Runtime.Managers;
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
        [ShowInInspector] private bool _isReadyToPlay;
        [ShowInInspector] private bool isLaunched = false;

        #endregion

        #endregion
        
        public void SetMovementData(BallData data) => _data = data;
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents() => BallSignals.Instance.onPlayConditionChanged += OnPlayConditionChanged;
        private void OnPlayConditionChanged(bool condition) => _isReadyToPlay = condition;

        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                rigidbody.gravityScale = 1;
                if (!isLaunched)
                {
                    LaunchBall();
                    isLaunched = true;
                }
            }
            else
            {
                rigidbody.gravityScale = 0;
                Stop();
                isLaunched = false;
            }
        }
        private void LaunchBall()
        {
            Vector2 force = new Vector2(Random.Range(-2f, 2f), 1);

            rigidbody.AddForce(force.normalized * _data.Speed, ForceMode2D.Impulse);
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