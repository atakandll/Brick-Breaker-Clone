using Runtime.Controllers.Ball;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class BallManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private BallMovementController movementController;
        
        #endregion

        #region Private Variables
        
        [ShowInInspector] private BallData _data;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetData();
            SendBallDataToControllers();
        }
        private BallData GetData() => Resources.Load<CD_Ball>("Data/CD_Ball").Data;
        private void SendBallDataToControllers() => movementController.SetMovementData(_data);
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += () => BallSignals.Instance.onPlayConditionChanged(true);
            CoreGameSignals.Instance.onLevelSuccessful += () => BallSignals.Instance.onPlayConditionChanged(true);
            CoreGameSignals.Instance.onLevelFailed += () => BallSignals.Instance.onPlayConditionChanged(false);
            CoreGameSignals.Instance.onReset += OnReset;
            
        }
        internal void OnInteractionPaddle()
        {
            Debug.Log("OnCollisionPaddle");

        }
      
        internal void OnInteractionEdge()
        {
            Debug.Log("OnCollisionEdge");
        }
        
        private void OnReset() => movementController.OnReset();
        private void OnDisable() => UnsubscribeEvents();

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= () => BallSignals.Instance.onPlayConditionChanged(true);
            CoreGameSignals.Instance.onLevelSuccessful -= () => BallSignals.Instance.onPlayConditionChanged(true);
            CoreGameSignals.Instance.onLevelFailed -= () => BallSignals.Instance.onPlayConditionChanged(false);
            CoreGameSignals.Instance.onReset -= OnReset;
            
        }
    }
}