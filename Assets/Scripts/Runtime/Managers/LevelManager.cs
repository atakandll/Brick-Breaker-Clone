using Runtime.Commands;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [Header("Holder")] [SerializeField] internal GameObject levelHolder;
        [Space] [SerializeField] private byte totalLevelCount;

        #endregion
        
        #region Private Variables

        private LevelLoaderCommand _levelLoader;
        private LevelDestroyerCommand _levelDestroyer;
        private byte _currentLevel;

        #endregion

        #endregion
        private void Awake() => Init();
        
        private void Init()
        {
            _levelLoader = new LevelLoaderCommand(this);
            _levelDestroyer = new LevelDestroyerCommand(this);
        }

        private void OnEnable() => SubscribeEvents();
      
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += _levelLoader.Execute;
            CoreGameSignals.Instance.onClearActiveLevel += _levelDestroyer.Execute;
            CoreGameSignals.Instance.onGetLevelID += GetLevelID;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
            
            //_currentLevel = GetLevelID()
            _currentLevel = 0; // for debugging
            CoreGameSignals.Instance.onLevelInitialize?.Invoke(_currentLevel);
        }

        private void OnDisable() => UnsubscribeEvents();

        private void UnsubscribeEvents()
        {
           CoreGameSignals.Instance.onLevelInitialize -= _levelLoader.Execute;
           CoreGameSignals.Instance.onClearActiveLevel -= _levelDestroyer.Execute;
           CoreGameSignals.Instance.onGetLevelID -= GetLevelID;
           CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
           CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
           
        }

        public void OnNextLevel()
        {
            _currentLevel++;
            // TODO: Save system
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke(GetLevelID());
        }

        public void OnRestartLevel()
        {
            // TODO: Save system
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke(GetLevelID());
            
        }

        public byte GetLevelID()
        {
            // TODO: Save system
            return 0;
        }
    }
}