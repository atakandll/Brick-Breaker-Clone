using System;
using DG.Tweening;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class BrickManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private DOTweenAnimation dotweenAnimation;

        #endregion

        #endregion
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents() => BallSignals.Instance.onInteractionEveryObject += OnInteraction;
      
        private void OnInteraction() => dotweenAnimation.DOPlay();

        private void OnDisable() => UnsubscribeEvents();

        private void UnsubscribeEvents() => BallSignals.Instance.onInteractionEveryObject -= OnInteraction;
       
    }
}