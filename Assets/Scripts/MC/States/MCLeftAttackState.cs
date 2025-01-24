using System;
using UnityEngine;

namespace TKM
{
    public class MCLeftAttackState : MCBaseState
    {
        string LEFT_ATTACK_ANIMATION_NAME = "Attack";
        bool _isNextInputAlreadyEnabled = false;
        public MCLeftAttackState(MCController _MCController) : base(_MCController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _isNextInputAlreadyEnabled = false;
            _MCController.SpriteRenderer.flipX = true;
            _MCController.Animator.Play(LEFT_ATTACK_ANIMATION_NAME);

            _MCController.OnAnimationFinished.AddListener(OnAnimationFinished);
        }

        public override void Update()
        {
            base.Update();

            if (_MCController.IsNextInputEnabled == true && _isNextInputAlreadyEnabled == false)
            {
                _isNextInputAlreadyEnabled = true;
                EnableBothAttack();
            }
        }

        public override void Exit()
        {
            base.Exit();

            DisableBothAttack();
            _MCController.OnAnimationFinished.RemoveListener(OnAnimationFinished);
        }

        public void OnAnimationFinished()
        {
            _MCController.SwitchState(_MCController.MCIdlingState);
        }
    }
}
