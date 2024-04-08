using DG.Tweening;
using Runtime.Controllers.Flash;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Ball
{
    public class BallSpriteController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private ParticleSystem confetti;
        [SerializeField] private new SpriteRenderer renderer;
       
        
        #endregion

        #region Private Variables

        private BallData _data;
        private Vector3 _originalScale;
        private Color _originalColor;

        #endregion

        #endregion
        private void Awake()
        {
            _originalScale = transform.localScale;
            _originalColor = renderer.color;
        }
        
        internal void SetData(BallData data)
        {
            _data = data;
        }

        internal void ScaleUpBall()
        {
            transform.DOScale(_data.ScaleUpSize, _data.ScaleUpDuration).SetEase(Ease.Flash)
                .OnComplete(() => transform.DOScale(_originalScale, _data.ScaleDownDuration));
            
        }

        internal void ChangeColorTemporarily()
        {
            renderer.DOColor(Color.white, _data.ColorChangeDuration).SetEase(Ease.Flash)
                .OnComplete(() => renderer.DOColor(_originalColor, _data.ColorChangeDuration));
        }
        internal void ShakeScreen()
        {
           CameraSignals.Instance.onCameraShake?.Invoke();
           Debug.Log("ShakeSignals Called");
        }
        internal void TriggerFlashEffect()
        {
            FindObjectOfType<FlashEffectController>().Flash();
        }
       

    }
}