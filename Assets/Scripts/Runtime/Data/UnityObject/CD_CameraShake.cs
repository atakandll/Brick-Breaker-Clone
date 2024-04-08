using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_CameraShake", menuName = "BrickBreaker/CD_CameraShake", order = 0)]
    public class CD_CameraShake : ScriptableObject
    {
        public CameraShakeData Data;
    }
}