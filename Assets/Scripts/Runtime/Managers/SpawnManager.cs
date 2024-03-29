using System;
using Runtime.Controllers.Ball;
using Runtime.Controllers.Bricks;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using Sirenix.OdinInspector.Editor.GettingStarted;
using Sirenix.OdinValidator.Editor;
using UnityEngine;

namespace Runtime.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public SpawnData Data;

        #endregion

        #region Private Variables

        private BricksSpawnController _bricksSpawnController;
        private BallSpawnController _ballSpawnController;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            Data = GetData();
        }
        private void GetReferences()
        {
            _bricksSpawnController = new BricksSpawnController(this);
            _ballSpawnController = new BallSpawnController(this);
        }
        private SpawnData GetData() => Resources.Load<CD_SpawnData>("Data/CD_Spawn").Data;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void OnPlay()
        {
            _bricksSpawnController.TriggerAction();
            _ballSpawnController.TriggerAction();
        }

        private void OnLevelInitialize(int a)
        {
            _bricksSpawnController.IsActivating = true;
            _ballSpawnController.IsActivating = true;
        }

        private void OnReset()
        {
            _bricksSpawnController.IsActivating = false;
            _ballSpawnController.IsActivating = false;
        }
    }
}