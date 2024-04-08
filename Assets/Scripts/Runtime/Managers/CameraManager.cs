using System;
using UnityEngine;
using DG.Tweening;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Signals;

namespace Runtime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform cameraTransform;
        
        #endregion

        #region Private Variables

        private CameraShakeData _data;

        #endregion

        #endregion

        private void Awake() => _data = GetData();

        private CameraShakeData GetData() => Resources.Load<CD_CameraShake>("Data/CD_CameraShake").Data;
        
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents() =>  ShakeSignals.Instance.onCameraShake += OnCameraShake;
    
        private void OnCameraShake()
        {
            cameraTransform.DOComplete();
            cameraTransform.DOShakePosition(0.3f, _data.positionStrength);
            cameraTransform.DOShakeRotation(0.3f , _data._rotationStrength);
        }

        private void OnDisable() => UnsubscribeEvents();

        private void UnsubscribeEvents()
        {
            ShakeSignals.Instance.onCameraShake -= OnCameraShake;
        }
    }
}
