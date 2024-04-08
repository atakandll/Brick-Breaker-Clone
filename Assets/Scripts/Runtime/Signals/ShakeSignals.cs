using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class ShakeSignals : MonoSingleton<ShakeSignals>
    {
        public UnityAction onBrickShake = delegate { };
        public UnityAction onEdgeShake = delegate { };
        public UnityAction onPaddleShake = delegate { };
        public UnityAction onCameraShake = delegate { };

    }
}