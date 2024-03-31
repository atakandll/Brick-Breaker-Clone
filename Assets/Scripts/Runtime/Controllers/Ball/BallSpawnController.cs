using System.Collections.Generic;
using System.Threading.Tasks;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Extensions;
using Runtime.Interfaces;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Ball
{
    public class BallSpawnController : ISpawner, IPullObject, IPushObject
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _spawnedObjects = new List<GameObject>();
        private SpawnManager _spawnManager;
        private BallSpawnData _data;
        #endregion

        #endregion
        public bool IsActivating { get; set; }

        public BallSpawnController(SpawnManager spawnManager)
        {
            _spawnManager = spawnManager;
            _data = _spawnManager.Data.BallSpawnData;
        }
        public void TriggerAction()
        {
            if (!IsActivating) return;
            SpawnDelay();
        }

        public async void SpawnDelay()
        {
            int milliSeconds = _spawnManager.Data.BallSpawnData.SpawnRange * 1000;
            for (int i = 0; i < _data.SpawnLimit; i++)
            {
                if(!IsActivating) break;
                await Task.Delay(milliSeconds);
                Spawn();
                
            }
        }
        public void Spawn()
        {
            GameObject ball = PullFromPool(PoolObjectType.Ball);
            _spawnedObjects.Add(ball);
            ball.transform.position = SelfExtensions.GetRandomPosition(_data.BallSpawnZone);
            
            BallMovementController ballMovement = ball.GetComponent<BallMovementController>();
            if(ballMovement != null)
            {
                ballMovement._isReadyToPlay = true;
                ballMovement.LaunchBall();
            }
        
            ball.SetActive(true);
            
        }

        public void Reset()
        {
            foreach (var ball in _spawnedObjects)
            {
                PushToPool(PoolObjectType.Ball,ball);
                
            }
        }

        public GameObject PullFromPool(PoolObjectType poolObjectType)
        {
            return PoolSignals.Instance.onGetPoolObject?.Invoke(poolObjectType);
        }

        public void PushToPool(PoolObjectType poolObjectType, GameObject obj)
        {
            PoolSignals.Instance.onReleasePoolObject?.Invoke(poolObjectType,obj);
        }
    }
}