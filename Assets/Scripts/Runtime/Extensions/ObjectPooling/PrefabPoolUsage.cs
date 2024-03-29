using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Extensions.ObjectPooling
{
    public class PrefabPoolUsage : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [Tooltip("The Pooler")] [SerializeField]
        private APooler Pooler;

        [Tooltip("The Pooled GameObject")] [SerializeField]
        private GameObject[] Prefabs;

        [Tooltip("Width between objects")] [SerializeField]
        private float distanceBetweenObjectsX;

        [Tooltip("Height between objects")] [SerializeField]
        private float distanceBetweenObjectsY;

        [SerializeField] private int objectsPerColumn;
        [SerializeField] private int objectsPerRow;
        
        #endregion

        #endregion
        
        private void Start()
        {
            for (int row = 0; row < objectsPerColumn ; row++) 
            {
                for (int column = 0; column < objectsPerRow; column++) 
                {
                    CreateObjectAtPosition(row, column);
                }
            }
        }
        private void CreateObjectAtPosition(int row, int column)
        {
            var randomIndex = Random.Range(0, Prefabs.Length);
            var randomObj = Prefabs[randomIndex];
            var obj = Pooler.Get<APooled>(randomObj);

            Vector3 newPosition = transform.position + new Vector3(
                column * distanceBetweenObjectsX, 
                -row * distanceBetweenObjectsY, 
                0); 

            obj.transform.position = newPosition;
            obj.transform.SetParent(transform);
        }

        [Button]
        public void PoolRandomObject()
        {
            var randomIndex = Random.Range(0, Prefabs.Length);
            var randomObj = Prefabs[randomIndex];
            var obj = Pooler.Get<APooled>(randomObj);
            obj.transform.SetParent(transform);
        }
        
        [Button]
        public void ReleaseChild(GameObject objToRelease)
        {
            if (objToRelease != null)
            {
                Pooler.Release(objToRelease);
            }
        }
    }
}