using Runtime.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class  BallSignals : MonoSingleton<BallSignals>
    {
        public UnityAction<bool> onPlayConditionChanged = delegate { };
        public UnityAction onInteractionEveryObject = delegate { };

    }
}