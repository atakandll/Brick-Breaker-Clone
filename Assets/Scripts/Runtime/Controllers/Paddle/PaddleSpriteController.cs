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
        
        internal void PlayConfetti()
        {
            confetti.Play();
        }
        
        
    }
}