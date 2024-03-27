using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Paddle", menuName = "BrickBreaker/CD_Paddle", order = 0)]
    public class CD_Paddle : ScriptableObject
    {
        public PaddleData Data;
    }
}