using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Extensions.ObjectPooling
{
    public class PrefabPooler : MonoBehaviour
    {
        private readonly Dictionary<GameObject, List<GameObject>> available =
            new Dictionary<GameObject, List<GameObject>>();

        private readonly Dictionary<GameObject, List<GameObject>> busy = new Dictionary<GameObject, List<GameObject>>();

        [Tooltip("All pooled models have to be inside this array before the initialization")] [SerializeField]
        private GameObject[] models;

        [Tooltip("How many objects will be created as soon as the game loads")] [SerializeField]
        private int startSize = 10;
        

        private void OnEnable()
        {
            if(!Application.isPlaying) return;

            Initialize();
        }
        private void Initialize()
        {
            if(models.Length == 0)
                Logger.Instance.Log<PrefabPooler>("Can't pool empty objects", "red");

            foreach (var model in models)
            {
                // List for pool
                var pool = new List<GameObject>();
                
                var busy = new List<GameObject>();

                for (var i = 0; i < startSize; i++)
                {
                    var obj = Instantiate(model, transform);
                    pool.Add(obj);
                    obj.SetActive(false);
                }
                
                available.Add(model,pool);
                this.busy.Add(model,busy);

            }
        }

        public virtual GameObject Get(GameObject model)
        {
            GameObject pooledObj = null;
            
            if(available == null) 
                Logger.Instance.Log<PrefabPooler>("PoolAble objects list is not created yet!", "green");
            
            if (busy == null)
                Logger.Instance.Log<PrefabPooler>("Nop! Busy objects list is not created yet!", "green");

            if (!available.ContainsKey(model))
                return null;
            
            //try to grab the last element of the available objects
            if (available[model].Count > 0)
            {
                var size = available[model].Count;
                pooledObj = available[model][size - 1];
            }
            
            if (pooledObj != null)
                available[model].Remove(pooledObj);
            else
                pooledObj = Instantiate(model, transform);
            
            busy[model].Add(pooledObj);
            pooledObj.SetActive(true);
            OnPool(pooledObj);
            return pooledObj;
        }
        public virtual T1 Get<T1>(GameObject prefabModel) where T1 : class
        {
            var obj = Get(prefabModel);
            return obj.GetComponent<T1>();
        }
        public virtual void Release(GameObject prefabModel, GameObject pooledObj)
        {
            if (available == null)
                Logger.Instance.Log<PrefabPooler>("Nop! PoolAble objects list is not created yet!", "green");

            if (busy == null)
                Logger.Instance.Log<PrefabPooler>("Nop! Busy objects list is not created yet!", "green");

            pooledObj.SetActive(false);
            busy[prefabModel].Remove(pooledObj);
            available[prefabModel].Add(pooledObj);
            pooledObj.transform.parent = transform;
            pooledObj.transform.localPosition = Vector3.zero;
            OnRelease(pooledObj);
        }
        
        // pool back to objects that you no longer use. They are deactived and stored back for future usage using the prefab model
        // as key to get back later on.
        public virtual void Release(GameObject pooledObj)
        {
            if(available == null)
                Logger.Instance.Log<PrefabPooler>("Nop! PoolAble objects list is not created yet!", "green");
            if(busy == null)
                Logger.Instance.Log<PrefabPooler>("Nop! Busy objects list is not created yet!", "green");
            
            pooledObj.SetActive(false);

            foreach (var model in busy.Keys)
            {
                if (busy[model].Contains(pooledObj))
                {
                    busy[model].Remove(pooledObj);
                    available[model].Add(pooledObj);
                    
                }
                
            }
            pooledObj.transform.parent = transform;
            pooledObj.transform.localPosition = Vector3.zero;
            OnRelease(pooledObj);
        }
        
        //Override this method to do something when before pool an object.
        protected virtual void OnPool(GameObject prefabModel)
        {
            
            // TODO: Clean references or reset variables are very common cases.
        }
        
        // Override this method to do something after release an object.
        protected virtual void OnRelease(GameObject prefabModel)
        {
           
            // TODO: Clean references or reset variables are very common cases.
        }
    }
}