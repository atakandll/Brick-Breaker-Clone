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

        private readonly List<GameObject> _spawnedObjects = new List<GameObject>();
        private readonly SpawnManager _spawnManager;
        private readonly BricksSpawnData _data;

        #endregion

        #endregion
        public bool IsActivating { get; set; }
        
        public BricksSpawnController(SpawnManager spawnManager)
        {
            _spawnManager = spawnManager;
            _data = _spawnManager.Data.BricksSpawnData;
            Debug.Log("BricksSpawnController Çalıştı");
            
        }
        public void TriggerAction()
        {
            if (!IsActivating) return;
            Spawn();
                
            Debug.Log("BricksSpawnController TriggerAction Çalıştı");
            
        }
        public void Spawn()
        {
            Debug.Log("Spanw method çalıştı");
            int totalBricksPerRow = _data.BricksPerRow;
            for (int i = 0; i < _data.SpawnLimit; i++)
            {
                if (!IsActivating) break;
                GameObject brick = PullFromPool(PoolObjectType.Bricks);
                int row = i / totalBricksPerRow;
                int column = i % totalBricksPerRow;
                Vector3 spawnPosition = SelfExtensions.GetPositionInGrid(row, column, _data.BrickSize, _data.Spacing, _data.OriginPosition);
                brick.transform.position = spawnPosition;
                _spawnedObjects.Add(brick);
            }
        }
        public GameObject PullFromPool(PoolObjectType poolObjectType)
        {
           return PoolSignals.Instance.onGetPoolObject(poolObjectType);
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