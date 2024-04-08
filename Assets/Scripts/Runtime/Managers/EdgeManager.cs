using System;
using DG.Tweening;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class EdgeManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables
        
        private EdgeShakeData _data;
        
        #endregion

        #endregion

        private void Awake() => _data = GetData();
        private EdgeShakeData GetData() => Resources.Load<CD_EdgeShake>("Data/CD_EdgeShake").Data;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
           ShakeSignals.Instance.onEdgeShake += OnEdgeShake;
        }

        private void OnEdgeShake()
        {
            transform.DOComplete();
            transform.DOShakePosition(0.2f, _data.positionStrength);
            transform.DOShakeRotation(0.2f , _data._rotationStrength);
        }
        private void UnsubscribeEvents()
        {
            ShakeSignals.Instance.onEdgeShake -= OnEdgeShake;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        
        
    }
}