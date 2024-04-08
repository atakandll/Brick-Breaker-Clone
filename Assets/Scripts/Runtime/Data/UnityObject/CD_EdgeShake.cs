using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_EdgeShake", menuName = "BrickBreaker/CD_EdgeShake", order = 0)]
    public class CD_EdgeShake : ScriptableObject
    {
        public EdgeShakeData Data;
    }
}