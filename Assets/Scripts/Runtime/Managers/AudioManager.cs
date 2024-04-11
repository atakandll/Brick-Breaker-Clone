using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class AudioManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip levelMusic;
        [SerializeField] private AudioClip paddleInteractionMusic;
        [SerializeField] private AudioClip[] brickInteractionMusic;
        [SerializeField] private AudioClip edgeInteractionMusic;
        [SerializeField] private  float comboTimer;
        
        #endregion

        #region Private Variables

        private float timer=0;
        private int comboCount=0;

        #endregion

        #endregion

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnLevelMusic;
            AudioSignals.Instance.onInteractionBrickSound += OnInteractionBrickSound;
            AudioSignals.Instance.onInteractionPaddleSound += OnInteractionPaddleSound;
            AudioSignals.Instance.onInteractionEdgeSound += OnInteractionEdgeSound;
        }
        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > comboTimer)
            {
                comboCount = 0;
            }
        }
        private void OnLevelMusic() => audioSource.PlayOneShot(levelMusic);
        private void OnInteractionBrickSound()
        {
            timer = 0;
            audioSource.PlayOneShot(brickInteractionMusic[comboCount]);

            if (comboCount < brickInteractionMusic.Length - 1)
                comboCount++;
        }
        private void OnInteractionPaddleSound() =>  audioSource.PlayOneShot(paddleInteractionMusic);
        private void OnInteractionEdgeSound() => audioSource.PlayOneShot(edgeInteractionMusic);

    }
}