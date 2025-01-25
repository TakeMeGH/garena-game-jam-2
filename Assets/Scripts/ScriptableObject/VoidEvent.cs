using UnityEngine;
using UnityEngine.Events;

namespace TKM
{
    [CreateAssetMenu(fileName = "VoidEvent", menuName = "Scriptable Objects/VoidEvent")]
    public class VoidEvent : ScriptableObject
    {
        public UnityAction EventAction;
        public void RaiseEvent()
        {
            EventAction?.Invoke();
        }
    }
}
