using UnityEngine;
using UnityEngine.Events;

namespace TKM
{
    [CreateAssetMenu(fileName = "VoidEvent", menuName = "Scriptable Objects/IntEvent")]
    public class IntEvent : ScriptableObject
    {
        public UnityAction<int> EventAction;
        public void RaiseEvent(int value)
        {
            EventAction?.Invoke(value);
        }
    }
}
