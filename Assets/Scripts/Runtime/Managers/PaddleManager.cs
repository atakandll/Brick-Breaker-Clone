﻿using System;
using Runtime.Controllers.Paddle;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;
using Logger = Runtime.Extensions.Logger;

namespace Runtime.Managers
{
    public class PaddleManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PaddleMovementController movementController;
        [SerializeField] private PaddlePhysicController physicController;

        #endregion

        #region Private Variables
        
        private PaddleData _data;
        
        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPaddleData();
            SendDataToControllers();
        }

        private PaddleData GetPaddleData() => Resources.Load<CD_Paddle>("Data/CD_Paddle").Data;
        private void SendDataToControllers() => movementController.SetMovementData(_data);
        private void OnEnable() => SubscribeEvents();
        
        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += () => PaddleSignals.Instance.onMoveConditionChanged?.Invoke(true);
            InputSignals.Instance.onInputReleased += () => PaddleSignals.Instance.onMoveConditionChanged?.Invoke(false);
            InputSignals.Instance.onInputDragged += OnInputDragged;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelSuccessful +=
                () => PaddleSignals.Instance.onPlayConditionChanged?.Invoke(true);
            CoreGameSignals.Instance.onLevelFailed +=
                () => PaddleSignals.Instance.onPlayConditionChanged?.Invoke(false);
            CoreGameSignals.Instance.onReset += OnReset;
            
        }
        private void OnPlay()
        {
            PaddleSignals.Instance.onPlayConditionChanged?.Invoke(true);
        }

        private void OnInputDragged(HorizontalInputParams inputParams)
        {
            movementController.UpdateInputValue(inputParams);
        }
        private void OnReset() => movementController.OnReset();
        
        private void OnDisable() => UnsubscribeEvents();

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= () => PaddleSignals.Instance.onMoveConditionChanged?.Invoke(true);
            InputSignals.Instance.onInputReleased -= () => PaddleSignals.Instance.onMoveConditionChanged?.Invoke(false);
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelSuccessful -=
                () => PaddleSignals.Instance.onPlayConditionChanged?.Invoke(true);
            CoreGameSignals.Instance.onLevelFailed -=
                () => PaddleSignals.Instance.onPlayConditionChanged?.Invoke(false);
            CoreGameSignals.Instance.onReset -= OnReset;
        }
    }
}