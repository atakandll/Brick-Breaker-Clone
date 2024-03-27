using Runtime.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class  BallSignals : MonoSingleton<BallSignals>
    {
        public UnityAction<bool> onPlayConditionChanged = delegate { };
        public UnityAction<GameObject> onInteractionPaddle = delegate { };
        public UnityAction<GameObject> onInteractionBrick = delegate { };
        public UnityAction<GameObject> onInteractionEdge = delegate { };
    }
}