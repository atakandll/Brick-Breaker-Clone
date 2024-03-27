using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Ball", menuName = "BrickBreaker/CD_Ball", order = 0)]
    public class CD_Ball : ScriptableObject
    {
        public BallData Data;
    }
}