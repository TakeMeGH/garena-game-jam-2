using UnityEngine;
using UnityEngine.Events;

namespace TKM
{
    public class MCAnimationEventTrigger : MonoBehaviour
    {
        public UnityEvent EnableMCNextInput;
        public UnityEvent AnimationFinish;

        public void TriggerEnableMCNextInput()
        {
            EnableMCNextInput?.Invoke();
        }

        public void TriggerAnimationFinish()
        {
            AnimationFinish?.Invoke();
        }
    }
}
