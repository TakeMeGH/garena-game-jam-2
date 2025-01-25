using System;
using UnityEngine;

namespace TKM
{
    public class Bomb : MonoBehaviour, IPerks
    {
        [SerializeField] VoidEvent _onLostCondition;

        public void Activate()
        {
            _onLostCondition.RaiseEvent();
            Destroy(gameObject);
        }
    }
}
