using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class PaddleSignals : MonoSingleton<PaddleSignals>
    {
        public UnityAction<bool> onPlayConditionChanged = delegate { };
        public UnityAction<bool> onMoveConditionChanged = delegate { };
        public UnityAction onInteractionWithBall = delegate { };
    }
}