using Runtime.Enums;
using UnityEngine;

namespace Runtime.Interfaces
{
    public interface IPullObject
    {
        GameObject PullFromPool(PoolObjectType poolObjectType);
    }
}