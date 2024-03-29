using System;
using Runtime.Enums;
using Runtime.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class PoolSignals : MonoSingleton<PoolSignals>
    {
        public Func<PoolObjectType,GameObject> onGetPoolObject = delegate { return null; };
        public UnityAction<PoolObjectType,GameObject> onReleasePoolObject = delegate { };
    }
}