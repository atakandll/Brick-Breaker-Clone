using System;
using DG.Tweening;
using Runtime.Controllers.Flash;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Controllers.Ball
{
    public class BallSpriteController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private ParticleSystem ballParticle;
        [SerializeField] private new SpriteRenderer renderer;
        [SerializeField] private TrailRenderer trailRenderer;
        
        #endregion

        #region Private Variables

        private BallData _data;
        private Vector3 _originalScale;
        private Color _originalColor;

        #endregion

        #endregion
        private void Awake() => GetOriginalValues();
        private void GetOriginalValues()
        {
            _originalScale = transform.localScale;
            _originalColor = renderer.color;
        }
        internal void SetData(BallData data) => _data = data;
      
        internal void ScaleUpBall()
        {
            transform.DOScale(_data.ScaleUpSize, _data.ScaleUpDuration).SetEase(Ease.Flash)
                .OnComplete(() => transform.DOScale(_originalScale, _data.ScaleDownDuration));
            
        }

        internal void ChangeBallColor()
        {
            trailRenderer.startColor = Color.white;
            trailRenderer.endColor = Color.white;
            renderer.DOColor(Color.white, _data.ColorChangeDuration)
                .OnComplete(() =>
                {
                    renderer.DOColor(_originalColor, _data.ColorChangeDuration);
                    trailRenderer.startColor = new Color(1f, 0.56f, 0f);
                    trailRenderer.endColor = new Color(1f, 0.56f, 0f);
                });
        }
        internal void PlayEdgeAndPaddleParticle()
        {
            if(ballParticle != null)
                ballParticle.Play();
        }

        internal void PlayBrickParticle()
        {
            
        }
        internal void ShakeScreen()
        {
           ShakeSignals.Instance.onCameraShake?.Invoke();
           Debug.Log("ShakeSignals Called");
        }
        internal void TriggerFlashEffect()
        {
            FindObjectOfType<FlashEffectController>().Flash();
        }

       
       

    }
}