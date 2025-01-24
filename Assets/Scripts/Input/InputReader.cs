using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TKM
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
    public class InputReader : ScriptableObject, MCInput.IGameplayActions, MCInput.IUIActions
    {
        #region Gameplay
        public Action<Vector2> MoveEvent;
        public Action JumpStarted;
        public Action JumpCanceled;
        public Action InteractPerformed;
        public Action PausePerformed;
        public Action LeftAttackPerformed;
        public Action RightAttackPerformed;
        #endregion

        #region UI
        public Action UnPausePerformed;
        #endregion


        MCInput _MCInput;
        void OnEnable()
        {
            if (_MCInput == null)
            {
                _MCInput = new MCInput();
                _MCInput.Gameplay.SetCallbacks(this);
                _MCInput.UI.SetCallbacks(this);
            }

            EnableGameplayInput();
        }

        void OnDisable()
        {
            if (_MCInput != null) _MCInput.Gameplay.Disable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void EnableGameplayInput()
        {
            _MCInput.Gameplay.Enable();
            _MCInput.UI.Disable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void EnableUIInput()
        {
            _MCInput.Gameplay.Disable();
            _MCInput.UI.Enable();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        #region Gameplay
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                JumpStarted?.Invoke();
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                JumpCanceled?.Invoke();
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                InteractPerformed?.Invoke();
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PausePerformed?.Invoke();
            }
        }

        public void OnLeftAttack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                LeftAttackPerformed?.Invoke();
            }
        }

        public void OnRightAttack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                RightAttackPerformed?.Invoke();
            }
        }
        #endregion

        #region UI

        public void OnUnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                UnPausePerformed?.Invoke();
            }
        }
        #endregion
    }
}
