using System;
using UnityEngine;

namespace TKM
{
    public class MCAttackState : MCBaseState
    {
        string LEFT_ATTACK_ANIMATION_NAME = "Attack";
        public MCAttackState(MCController _MCController) : base(_MCController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _MCController.SpriteRenderer.flipX = true;
            _MCController.Animator.Play(LEFT_ATTACK_ANIMATION_NAME);

            _MCController.OnAnimationFinished += OnAnimationFinished;
            _MCController.EnableNextInput += OnEnableNextInput;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();

            DisableBothAttack();
            _MCController.OnAnimationFinished -= OnAnimationFinished;
            _MCController.EnableNextInput -= OnEnableNextInput;
        }

        public void OnAnimationFinished()
        {
            _MCController.SwitchState(_MCController.MCIdlingState);
        }

        public void OnEnableNextInput()
        {
            EnableBothAttack();
        }
    }
}
