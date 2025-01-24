using System;
using UnityEngine;

namespace TKM
{
    public class MCAttackState : MCBaseState
    {
        bool _isInputEnable = false;
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
        }

        public override void Update()
        {
            base.Update();

            if (_isInputEnable)
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
            _MCController.SwitchState(_MCController.MCIdlingState);
        }

        public void OnEnableNextInput()
        {
            _isInputEnable = true;
        }
    }
}
