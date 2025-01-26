using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TKM
{
    public enum TutorialType
    {
        BasicGameKnowledge,
        Perks,
        Bombs,
        AttackedSpool
    }
    public class TutorialManager : Singleton<TutorialManager>
    {
        public bool[] IsTutorialShowedUp { get; private set; }
        public IntEvent TriggerTutorial;
        Coroutine _currentCourutine;
        Queue<TutorialType> TutorialQueue = new Queue<TutorialType>();
        private void Start()
        {
            IsTutorialShowedUp = new bool[Enum.GetValues(typeof(TutorialType)).Length];

            for (int i = 0; i < IsTutorialShowedUp.Length; i++)
            {
                IsTutorialShowedUp[i] = false;
            }
        }
        public void ShowTutorial(TutorialType type)
        {
            if (IsTutorialShowedUp[(int)type]) return;

            IsTutorialShowedUp[(int)type] = true;
            if (_currentCourutine != null || TutorialQueue.Count > 0)
            {
                TutorialQueue.Enqueue(type);
            }
            else
            {
                _currentCourutine = StartCoroutine(TriggerEventAfterDelay(1f, type));
            }
        }

        IEnumerator TriggerEventAfterDelay(float delay, TutorialType type)
        {
            yield return new WaitForSeconds(delay);
            TriggerTutorial.RaiseEvent((int)type);
            _currentCourutine = null;
            ProceedNextQueue();

        }

        void ProceedNextQueue()
        {
            if (TutorialQueue.Count == 0) return;
            TutorialType type = (TutorialType)TutorialQueue.Dequeue();
            _currentCourutine = StartCoroutine(TriggerEventAfterDelay(1f, type));
        }

    }
}
