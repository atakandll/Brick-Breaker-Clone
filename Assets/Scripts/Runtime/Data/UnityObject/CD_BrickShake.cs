using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_BrickShake", menuName = "BrickBreaker/CD_BrickShake", order = 0)]
    public class CD_BrickShake : ScriptableObject
    {
        public BrickShakeData Data;
    }
}