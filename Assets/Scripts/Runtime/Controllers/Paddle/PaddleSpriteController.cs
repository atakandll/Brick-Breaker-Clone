using DG.Tweening;
using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Controllers.Paddle
{
    public class PaddleSpriteController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private ParticleSystem confetti;
        [SerializeField] private new SpriteRenderer renderer;

        #endregion

        #endregion
        
        private PaddleData _data;
        
        internal void SetSpriteData(PaddleData data) => _data = data;
        internal void PlayConfetti() => confetti.Play();
        internal void ShakePaddle()
        {
            transform.DOComplete();
            transform.DOShakePosition(0.2f, _data.positionStrength);
            transform.DOShakeRotation(0.2f , _data._rotationStrength);
        }
    }
}