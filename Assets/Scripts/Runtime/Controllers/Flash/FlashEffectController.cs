using DG.Tweening;
using UnityEngine;

namespace Runtime.Controllers.Flash
{
    public class FlashEffectController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CanvasGroup flashPanel;

        #endregion

        #endregion
        
        private void Start()
        {
            SetValues();
        }
        private void SetValues()
        {
            flashPanel.alpha = 0f;
            flashPanel.blocksRaycasts = false;
            flashPanel.interactable = false;
        }

        internal void Flash()
        {
            flashPanel.DOFade(1, 0.1f).SetEase(Ease.Flash).OnComplete(() =>
            {
                flashPanel.DOFade(0, 0.3f).SetEase(Ease.Flash);
                flashPanel.blocksRaycasts = false; 
            });
        }
    }
}