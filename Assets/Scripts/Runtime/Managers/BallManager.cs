using System;
using Runtime.Controllers.Ball;
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
        [SerializeField] private BallPhysicController physicController;

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
        private void SendBallDataToControllers() => movementController.SetMovementData(_data);
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += () => BallSignals.Instance.onPlayConditionChanged(true);
            CoreGameSignals.Instance.onLevelSuccessful += () => BallSignals.Instance.onPlayConditionChanged(true);
            CoreGameSignals.Instance.onLevelFailed += () => BallSignals.Instance.onPlayConditionChanged(false);
            CoreGameSignals.Instance.onReset += OnReset;
            
            BallSignals.Instance.onInteractionPaddle += OnInteractionPaddle;
            BallSignals.Instance.onInteractionBrick += OnInteractionBrick;
            BallSignals.Instance.onInteractionEdge += OnInteractionEdge;
        }

        private void OnInteractionPaddle(GameObject paddleGameObject)
        {
            _data.Speed -= 5;
        }
        private void OnInteractionBrick(GameObject brickGameObject)
        {
            //TODO: Add Feel
            _data.Speed += 7;

        }
        private void OnInteractionEdge(GameObject edgeGameObject)
        {
            _data.Speed += 5;
        }


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