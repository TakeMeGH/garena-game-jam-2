using System;
using UnityEngine;

namespace TKM
{
    public class MCAttackState : MCBaseState
    {
        bool _isInputEnable = false;
        int _attackCall = 0;
        public MCAttackState(MCController _MCController) : base(_MCController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            // Ganti scale Y
            if (_MCController.NextAttackFacing == 1)
            {
                _MCController.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (_MCController.NextAttackFacing == -1)
            {
                _MCController.transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            _MCController.Animator.SetTrigger(_MCController.NextAttackAnimation);
            _MCController.transform.position = (Vector3)_MCController.NextPosition;

            _MCController.OnAnimationFinished += OnAnimationFinished;
            _MCController.EnableNextInput += OnEnableNextInput;

            PlaySound();
        }

        public override void Update()
        {
            base.Update();

            if (_isInputEnable && _MCController.IsAttackMiss == false)
            {
                ProceedInput();
            }
        }

        public override void Exit()
        {
            base.Exit();

            _isInputEnable = false;
            _MCController.OnAnimationFinished -= OnAnimationFinished;
            _MCController.EnableNextInput -= OnEnableNextInput;
        }

        public void OnAnimationFinished()
        {
            if (_MCController.IsAttackMiss)
            {
                _MCController.SwitchState(_MCController.MCMissState);
            }
            else
            {
                _MCController.SwitchState(_MCController.MCIdlingState);
            }
        }

        public void OnEnableNextInput()
        {
            if (_MCController.IsAttackMiss == false)
            {
                _MCController.NextEnemy.OnHitEnemy();
            }
            _isInputEnable = true;
        }

        void PlaySound()
        {
            if (_MCController.InputType == InputType.Keyboard)
            {
                AudioManager.Instance.PlaySFX(SFX.PlayerB_atk1);
            }
            else
            {
                switch (_attackCall % 3)
                {
                    case 0:
                        AudioManager.Instance.PlaySFX(SFX.PlayerA_atk1);
                        break;
                    case 1:
                        AudioManager.Instance.PlaySFX(SFX.PlayerA_atk2);
                        break;
                    case 2:
                        AudioManager.Instance.PlaySFX(SFX.PlayerA_atk3);
                        break;
                }
                _attackCall++;
            }
        }
    }
}
