using Runtime.Enums;
using UnityEngine;

namespace Runtime.Interfaces
{
    public interface IPushObject
    {
        void PushToPool(PoolObjectType poolObjectType,GameObject obj);
    }
}