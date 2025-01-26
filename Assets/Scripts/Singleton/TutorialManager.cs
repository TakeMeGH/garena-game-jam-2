using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

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
        bool _isOnDialogue = false;
        Queue<TutorialType> TutorialQueue = new Queue<TutorialType>();
        public InputReader InputReader;
        DialogueRunner _dialogueRunner;
        float DELAY = 2f;
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
            if (type == TutorialType.AttackedSpool && Spawner.Instance.IsLastWave()) return;
            if (IsTutorialShowedUp[(int)type]) return;

            IsTutorialShowedUp[(int)type] = true;
            if (_isOnDialogue || TutorialQueue.Count > 0)
            {
                TutorialQueue.Enqueue(type);
            }
            else
            {
                StartCoroutine(TriggerEventAfterDelay(DELAY, type));
            }
        }

        IEnumerator TriggerEventAfterDelay(float delay, TutorialType type)
        {
            _isOnDialogue = true;
            yield return new WaitForSeconds(delay);
            InputReader.EnableUIInput();
            _dialogueRunner = FindAnyObjectByType<DialogueRunner>();
            _dialogueRunner.StartDialogue("Tutorial_" + ((int)type + 1));
            _dialogueRunner.onNodeComplete.AddListener(ProceedNextQueue);
            Time.timeScale = 0.1f;
            // TriggerTutorial.RaiseEvent((int)type);
            // _currentCourutine = null;

        }

        private void ProceedNextQueue(string arg0)
        {
            Time.timeScale = 1f;

            InputReader.EnableGameplayInput();
            _isOnDialogue = false;
            if (_dialogueRunner != null) _dialogueRunner.onNodeComplete.RemoveAllListeners();
            if (TutorialQueue.Count == 0) return;
            TutorialType type = TutorialQueue.Dequeue();
            StartCoroutine(TriggerEventAfterDelay(DELAY, type));
        }

        // void ProceedNextQueue()
        // {
        //     if (TutorialQueue.Count == 0) return;
        //     TutorialType type = TutorialQueue.Dequeue();
        //     _currentCourutine = StartCoroutine(TriggerEventAfterDelay(1f, type));
        // }

    }
}
