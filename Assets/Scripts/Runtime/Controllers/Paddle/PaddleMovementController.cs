using System;
using Runtime.Data.ValueObject;
using Runtime.Keys;
using Runtime.Managers;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Logger = Runtime.Extensions.Logger;

namespace Runtime.Controllers.Paddle
{
    public class PaddleMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody2D rigidbody;

        #endregion

        #region Private Variables

        [Header("Data")][ShowInInspector] private PaddleData _data;
        [ShowInInspector] private bool _isReadyToMove, _isReadyToPlay;
        [ShowInInspector] private float _inputValue;
        [ShowInInspector] private Vector2 _clampValues;

        #endregion

        #endregion
        internal void SetMovementData(PaddleData data) => _data = data;
        private void OnEnable()
        {
            SubscribeEvents();
            Logger.Instance.Log<PaddleMovementController>("OnEnableMovementController", "green");
        }

        private void SubscribeEvents()
        {
            PaddleSignals.Instance.onPlayConditionChanged += OnPlayConditionChanged;
            PaddleSignals.Instance.onMoveConditionChanged += OnMoveConditionChanged;
        }

        private void OnMoveConditionChanged(bool condition) => _isReadyToMove = condition;
        private void OnPlayConditionChanged(bool condition) => _isReadyToPlay = condition;
        private void OnDisable() => UnsubscribeEvents();
        private void UnsubscribeEvents()
        {
            PaddleSignals.Instance.onPlayConditionChanged -= OnPlayConditionChanged;
            PaddleSignals.Instance.onMoveConditionChanged -= OnMoveConditionChanged;
        }
        public void UpdateInputValue(HorizontalInputParams inputParams)
        {
            _inputValue = inputParams.HorizontalInputValue;
            _clampValues = inputParams.HorizontalInputClampSides;
        }
        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                if (_isReadyToMove)
                {
                    Move();
                }
                else
                {
                    StopSideways();
                }
            }
            else
                Stop();
        }
        
        private void Move()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_inputValue * _data.SideWaysSpeed, velocity.y);
            rigidbody.velocity = velocity;

            Vector2 position;
            position = new Vector2(Mathf.Clamp(rigidbody.position.x, _clampValues.x, _clampValues.y),
                rigidbody.position.y);
            rigidbody.position = position;

        }
        private void StopSideways()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(0, velocity.y);
            rigidbody.velocity = velocity;
        }
        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
        }

        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
        
    }
}