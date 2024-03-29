using System.Collections.Generic;
using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_ObjectPoolData", menuName = "BrickBreaker/CD_ObjectPoolData", order = 0)]
    public class CD_ObjectPoolData : ScriptableObject
    {
        public List<ObjectPoolData> ObjectData;
    }
}