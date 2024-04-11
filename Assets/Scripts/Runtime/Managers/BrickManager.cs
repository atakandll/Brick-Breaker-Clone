using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class BrickManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private ParticleSystem brickParticle;


        #endregion
        
        #region Private Variables
        
        private BrickShakeData _data;
        
        #endregion
        
        #endregion
        
        private void Awake()
        {
            GetReference();
        }
        private void GetReference()
        {
            _data = GetData();
        }

        private BrickShakeData GetData() => Resources.Load<CD_BrickShake>("Data/CD_BrickShake").Data;

        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            ShakeSignals.Instance.onBrickShake += OnBrickShake;
        }
        internal void TriggerParticleEffect()
        {
           
            if (brickParticle != null)
            {
                Debug.Log("Particle oynatılıyor.");
                brickParticle.Play();
            }
            else
            {
                Debug.Log("ParticleSystem referansı bulunamadı.");
            }
        }

        private void OnBrickShake()
        {
            transform.DOComplete();
            transform.DOShakePosition(0.2f, _data.positionStrength);
            transform.DOShakeRotation(0.2f , _data._rotationStrength);
           
        }
        internal void DestroyBrick()
        {
            brickParticle.Play();
            StartCoroutine(ReturnToPoolAfterParticles());
        }

        private IEnumerator ReturnToPoolAfterParticles()
        {
            // Particulların oynatılma süresini bekle
            yield return new WaitForSeconds(brickParticle.main.duration);
        
            // Particullar bittiğinde, objeyi pool'a geri döndür
            PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolObjectType.Bricks, gameObject);
        }

        private void OnDisable() => UnsubscribeEvents();

        private void UnsubscribeEvents()
        {
            ShakeSignals.Instance.onBrickShake -= OnBrickShake;

        }
    }
}