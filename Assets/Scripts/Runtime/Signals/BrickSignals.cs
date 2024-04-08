using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class BrickSignals: MonoSingleton<BrickSignals>
    {
        public UnityAction onBrickShake = delegate { };

    }
}