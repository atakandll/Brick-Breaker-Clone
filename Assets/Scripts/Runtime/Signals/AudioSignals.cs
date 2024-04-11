using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class AudioSignals : MonoSingleton<AudioSignals>
    {
        public UnityAction onInteractionPaddleSound = delegate {  };
        public UnityAction onInteractionBrickSound = delegate { };
        public UnityAction onInteractionEdgeSound = delegate { };
    }
}