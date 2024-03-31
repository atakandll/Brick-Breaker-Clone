using System;
using System.Collections.Generic;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.Rendering;

namespace Runtime.Managers
{
    public class PoolManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private SerializedDictionary<PoolObjectType, Queue<GameObject>> objectPool;
        [SerializeField] private GameObject poolholder;
        [SerializeField] private CD_ObjectPoolData data;

        #endregion

        #region Private Variables

        private ObjectPoolData _data;
        private GameObject _getObjectFromPool;
        private readonly int _loadPoolCount = Enum.GetNames(typeof(PoolObjectType)).Length;

        #endregion

        #endregion

        private int poolCount = 0;

        private void Awake() => PoolGenerator();
        private void PoolGenerator()
        {
            Debug.Log("Pool Generator Çalıştı");
            objectPool = new SerializedDictionary<PoolObjectType, Queue<GameObject>>();

            for (; poolCount < _loadPoolCount; poolCount++)
            {
                objectPool.Add(data.ObjectData[poolCount].PoolObjectType, new Queue<GameObject>());

                for (int j = 0; j < data.ObjectData[poolCount].PoolSize; j++)
                {
                    var poolObj = Instantiate(data.ObjectData[poolCount].PoolObject, poolholder.transform);

                    poolObj.SetActive(false);

                    objectPool[data.ObjectData[poolCount].PoolObjectType].Enqueue(poolObj);
                }
            }
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            PoolSignals.Instance.onGetPoolObject += OnGetPoolObject;
            PoolSignals.Instance.onReleasePoolObject += OnReleasePoolObject;
        }

        private void UnsubscribeEvents()
        {
            PoolSignals.Instance.onGetPoolObject -= OnGetPoolObject;
            PoolSignals.Instance.onReleasePoolObject -= OnReleasePoolObject;
        }

        private void OnDisable() => UnsubscribeEvents();
        

        private GameObject OnGetPoolObject(PoolObjectType type)
        {
            Debug.Log("Pulldan Çektik AgaNiga");
            if (objectPool[type].Count == 0 && data.ObjectData[(int)type].PoolType == PoolType.Dynamic)
            {
                _getObjectFromPool = Instantiate(data.ObjectData[(int)type].PoolObject, poolholder.transform);
            }
            else
            {
                if (objectPool[type].Count != 0)
                {
                    _getObjectFromPool = objectPool[type].Peek();

                    _getObjectFromPool.SetActive(true);

                    objectPool[type].Dequeue();
                }
            }

            return _getObjectFromPool;
        }

        private void OnReleasePoolObject(PoolObjectType type, GameObject obj)
        {
            obj.SetActive(false);

            objectPool[type].Enqueue(obj);
        }
    }
}