﻿using System;
using System.Collections.Generic;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Keys;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using Logger = Runtime.Extensions.Logger;

namespace Runtime.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private float _positionValuesX;
        private bool _isTouching;

        private float _currentVelocity; // ref type
        private Vector2? _mousePosition; // ref type
        private Vector3 _moveVector; // ref type

        [Header("Data")] private InputData _data;
        [ShowInInspector] private bool _isFirstTimeTouchTaken;
        [ShowInInspector] private bool _isAvaibleForTouch;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetInputData();
            Logger.Instance.Log<InputManager>("InputData Loaded", "green", _data.GetType());
           
        }
        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").Data;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            Logger.Instance.Log<InputManager>("InputEvents Subscribed", "red");
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
            InputSignals.Instance.onChangeInputState += OnChangeInputState;
        }
        private void OnPlay() => _isAvaibleForTouch = true;
        private void OnChangeInputState(bool state) => _isAvaibleForTouch = state;

        private void OnDisable() => UnsubscribeEvents();

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            InputSignals.Instance.onChangeInputState -= OnChangeInputState;
        }

        private void Update()
        {
            if (!_isAvaibleForTouch) return;

            if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
            {
                _isTouching = false;
                InputSignals.Instance.onInputReleased?.Invoke();
                Logger.Instance.Log<InputManager>("Input Released", "red");

            }

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {
                _isTouching = true;

                InputSignals.Instance.onInputTaken?.Invoke();

                if (!_isFirstTimeTouchTaken)
                {
                    _isFirstTimeTouchTaken = true;
                    InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
                    Logger.Instance.Log<InputManager>("First Time Touch", "red");

                }
                _mousePosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
            {
                if (_isTouching)
                {
                    if (_mousePosition != null)
                    {
                        Vector2 mouseDeltaPos =
                            (Vector2)Input.mousePosition -
                            _mousePosition.Value;
                        
                        if (mouseDeltaPos.x > _data.HorizontalInputSpeed)
                            _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                        else if (mouseDeltaPos.x < -_data.HorizontalInputSpeed)
                            _moveVector.x = -_data.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
                        else
                            _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                                _data.HorizontalInputClampStopValue);

                        _mousePosition = Input.mousePosition;

                        InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                        {
                            HorizontalInputValue = _moveVector.x,
                            HorizontalInputClampSides = _data.HorizontalInputClampNegativeSides,

                        });
                        Logger.Instance.Log<InputManager>("Input Dragged", "red");
                    }
                }
            }
        }
        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
        private void OnReset()
        {
            _isTouching = false;
            _isFirstTimeTouchTaken = false;
        }
    }
}