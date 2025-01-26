using System;
using System.Collections;
using UnityEngine;

namespace TKM
{
    public class InGameUIController : MonoBehaviour
    {
        [SerializeField] InputReader _inputReader;
        [Header("Menu")]
        [SerializeField] CanvasGroup _pauseMenu;
        [SerializeField] CanvasGroup _pauseMouseMenu;
        [SerializeField] CanvasGroup _loseMenu;
        [SerializeField] CanvasGroup _winMenu;
        [SerializeField] CanvasGroup _overlay;
        [SerializeField] CanvasGroup[] _tutorialMenu;
        CanvasGroup _selectedTutorial;

        [Header("Event")]
        [SerializeField] VoidEvent _onLoseConditions;
        [SerializeField] VoidEvent _onWinConditions;
        [SerializeField] IntEvent _triggerTutorial;


        void OnEnable()
        {
            _inputReader.PausePerformed += OnPause;
            _inputReader.PauseMousePerformed += OnPauseMouse;
            _onLoseConditions.EventAction += OnLoseConditions;
            _onWinConditions.EventAction += OnWinConditions;
            _triggerTutorial.EventAction += OnTriggerTutorial;
        }

        void OnDisable()
        {
            _inputReader.PausePerformed -= OnPause;
            _inputReader.PauseMousePerformed -= OnPauseMouse;
            _onLoseConditions.EventAction -= OnLoseConditions;
            _onWinConditions.EventAction -= OnWinConditions;
            _triggerTutorial.EventAction -= OnTriggerTutorial;
        }

        private void OnPause()
        {
            _overlay.alpha = 0f;
            _pauseMenu.alpha = 1;
            _pauseMenu.blocksRaycasts = true;
            _pauseMenu.interactable = true;

            _inputReader.EnableUIInput();
            Time.timeScale = 0f;
        }

        private void OnPauseMouse()
        {
            _overlay.alpha = 0f;
            _pauseMouseMenu.alpha = 1;
            _pauseMouseMenu.blocksRaycasts = true;
            _pauseMouseMenu.interactable = true;

            _inputReader.EnableUIInput();
            Time.timeScale = 0f;
        }


        public void OnUnPause()
        {
            Time.timeScale = 1f;
            _inputReader.EnableGameplayInput();
            _pauseMenu.alpha = 0;
            _pauseMenu.blocksRaycasts = false;
            _pauseMenu.interactable = false;
            _overlay.alpha = 1f;
        }

        public void OnUnPauseMouse()
        {
            Time.timeScale = 1f;
            _inputReader.EnableGameplayInput();
            _pauseMouseMenu.alpha = 0;
            _pauseMouseMenu.blocksRaycasts = false;
            _pauseMouseMenu.interactable = false;
            _overlay.alpha = 1f;
        }
        private void OnWinConditions()
        {
            StartCoroutine(HandleWinWithDelay());
        }

        IEnumerator HandleWinWithDelay()
        {
            float delayTime = 2f;
            yield return new WaitForSecondsRealtime(delayTime);

            _overlay.alpha = 0f;
            AudioManager.Instance.PlayBGM(BGM.WinCond);
            _winMenu.alpha = 1;
            _winMenu.blocksRaycasts = true;
            _winMenu.interactable = true;

            _inputReader.EnableUIInput();
        }
        private void OnLoseConditions()
        {
            StartCoroutine(HandleLoseWithDelay());
        }

        private IEnumerator HandleLoseWithDelay()
        {
            float delayTime = 2f;
            yield return new WaitForSecondsRealtime(delayTime);

            _overlay.alpha = 0f;
            AudioManager.Instance.PlayBGM(BGM.LoseCond);
            _loseMenu.alpha = 1;
            _loseMenu.blocksRaycasts = true;
            _loseMenu.interactable = true;

            _inputReader.EnableUIInput();
        }
        void OnTriggerTutorial(int index)
        {
            _overlay.alpha = 0f;

            _selectedTutorial = _tutorialMenu[index];
            _selectedTutorial.alpha = 1;
            _selectedTutorial.blocksRaycasts = true;
            _selectedTutorial.interactable = true;

            _inputReader.EnableUIInput();
            Time.timeScale = 0f;
        }

        public void OnCloseTutorial()
        {
            Time.timeScale = 1f;
            _inputReader.EnableGameplayInput();

            _selectedTutorial.alpha = 0;
            _selectedTutorial.blocksRaycasts = false;
            _selectedTutorial.interactable = false;

            _overlay.alpha = 1f;

        }



    }
}
