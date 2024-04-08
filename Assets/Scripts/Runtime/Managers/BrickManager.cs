using System;
using DG.Tweening;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class BrickManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables
        
        private BrickShakeData _data;
        
        #endregion
        
        #endregion
        
        private void Awake() => _data = GetData();

        private BrickShakeData GetData() => Resources.Load<CD_BrickShake>("Data/CD_BrickShake").Data;

        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents() => ShakeSignals.Instance.onBrickShake += OnBrickShake;

        private void OnBrickShake()
        {
            transform.DOComplete();
            transform.DOShakePosition(0.2f, _data.positionStrength);
            transform.DOShakeRotation(0.2f , _data._rotationStrength);
           
        }

        private void OnDisable() => UnsubscribeEvents();

        private void UnsubscribeEvents() => BallSignals.Instance.onInteractionEveryObject -= OnBrickShake;
       
    }
}