using System;
using UnityEngine;

namespace TKM
{
    public class InGameUIController : MonoBehaviour
    {
        [SerializeField] InputReader _inputReader;
        [Header("Pause Menu")]
        [SerializeField] CanvasGroup _pauseMenu;
        [SerializeField] CanvasGroup _pauseMouseMenu;
        [SerializeField] CanvasGroup _loseMenu;
        [SerializeField] CanvasGroup _winMenu;
        [Header("Event")]
        [SerializeField] VoidEvent _onLoseConditions;
        [SerializeField] VoidEvent _onWinConditions;

        void OnEnable()
        {
            _inputReader.PausePerformed += OnPause;
            _inputReader.PauseMousePerformed += OnPauseMouse;
            _onLoseConditions.EventAction += OnLoseConditions;
            _onWinConditions.EventAction += OnWinConditions;
        }


        void OnDisable()
        {
            _inputReader.PausePerformed -= OnPause;
            _inputReader.PauseMousePerformed -= OnPauseMouse;
            _onLoseConditions.EventAction -= OnLoseConditions;
            _onWinConditions.EventAction -= OnWinConditions;


        }

        private void OnPause()
        {
            _pauseMenu.alpha = 1;
            _pauseMenu.blocksRaycasts = true;
            _pauseMenu.interactable = true;

            _inputReader.EnableUIInput();
            Time.timeScale = 0f;
        }

        private void OnPauseMouse()
        {
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
        }

        public void OnUnPauseMouse()
        {
            Time.timeScale = 1f;
            _inputReader.EnableGameplayInput();
            _pauseMouseMenu.alpha = 0;
            _pauseMouseMenu.blocksRaycasts = false;
            _pauseMouseMenu.interactable = false;
        }

        private void OnWinConditions()
        {
            AudioManager.Instance.PlayBGM(BGM.WinCond);
            _winMenu.alpha = 1;
            _winMenu.blocksRaycasts = true;
            _winMenu.interactable = true;

            _inputReader.EnableUIInput();
        }

        private void OnLoseConditions()
        {
            AudioManager.Instance.PlayBGM(BGM.LoseCond);
            _loseMenu.alpha = 1;
            _loseMenu.blocksRaycasts = true;
            _loseMenu.interactable = true;

            _inputReader.EnableUIInput();
        }


    }
}
