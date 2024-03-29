using System.Collections.Generic;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Extensions;
using Runtime.Interfaces;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Bricks
{
    public class BricksSpawnController : ISpawner,IPullObject,IPushObject
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _spawnedObjects = new List<GameObject>();
        private SpawnManager _spawnManager;
        private BricksSpawnData _data;

        #endregion

        #endregion
        public bool IsActivating { get; set; }
        
        public BricksSpawnController(SpawnManager spawnManager)
        {
            _spawnManager = spawnManager;
            _data = _spawnManager.Data.BricksSpawnData;
            
        }
        public void TriggerAction()
        {
            if (!IsActivating) return;
            
            for (int i = 0; i < _data.SpawnLimit; i++)
            {
                if (!IsActivating) break;
                Spawn();
            }
        }
        public void Spawn()
        {
            GameObject bricks = PullFromPool(PoolObjectType.Bricks);
            _spawnedObjects.Add(bricks);
            bricks.transform.position = SelfExtensions.GetRandomPosition(_data.BrickSpawnZone);
        }
        public GameObject PullFromPool(PoolObjectType poolObjectType)
        {
           return PoolSignals.Instance.onGetPoolObject?.Invoke(poolObjectType);
        }
        public void Reset()
        {
            foreach (var brick in _spawnedObjects)
            {
                PushToPool(PoolObjectType.Bricks,brick);
                
            }
        }
        public void PushToPool(PoolObjectType poolObjectType, GameObject obj)
        {
            PoolSignals.Instance.onReleasePoolObject?.Invoke(poolObjectType,obj);
        }
    }
}