﻿using Runtime.Controllers.Ball;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class BallManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private BallMovementController movementController;
        [SerializeField] private BallSpriteController spriteController;
        
        #endregion

        #region Private Variables
        
        [ShowInInspector] private BallData _data;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetData();
            SendBallDataToControllers();
        }
        private BallData GetData() => Resources.Load<CD_Ball>("Data/CD_Ball").Data;
        private void SendBallDataToControllers()
        {
            movementController.SetMovementData(_data);
            spriteController.SetData(_data);
        }

        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += () => BallSignals.Instance.onPlayConditionChanged(true);
            CoreGameSignals.Instance.onLevelSuccessful += () => BallSignals.Instance.onPlayConditionChanged(true);
            CoreGameSignals.Instance.onLevelFailed += () => BallSignals.Instance.onPlayConditionChanged(false);
            CoreGameSignals.Instance.onReset += OnReset;
        }
        internal void OnInteractionWithBricks()
        {
            TriggerBrickShake();
            ApplyVisualEffects();
            TriggerFlashEffect();
        }
        internal void OnInteractionPaddle()
        {
            ShakeSignals.Instance.onPaddleShake?.Invoke();
            TriggerBrickShake();
            ApplyVisualEffects();
        }
        internal void OnInteractionEdge()
        {
            TriggerEdgeShake();
            TriggerBrickShake();
            ApplyVisualEffects();
        }

        private void TriggerBrickShake() => ShakeSignals.Instance.onBrickShake?.Invoke();

        private void TriggerEdgeShake() => ShakeSignals.Instance.onEdgeShake?.Invoke();
       
        private void ApplyVisualEffects()
        {
            spriteController.ScaleUpBall();
            spriteController.ChangeColorTemporarily();
            spriteController.ShakeScreen();
        }
        private void TriggerFlashEffect() => spriteController.TriggerFlashEffect();

        private void OnReset() => movementController.OnReset();
        private void OnDisable() => UnsubscribeEvents();

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= () => BallSignals.Instance.onPlayConditionChanged(true);
            CoreGameSignals.Instance.onLevelSuccessful -= () => BallSignals.Instance.onPlayConditionChanged(true);
            CoreGameSignals.Instance.onLevelFailed -= () => BallSignals.Instance.onPlayConditionChanged(false);
            CoreGameSignals.Instance.onReset -= OnReset;
            
        }
    }
}