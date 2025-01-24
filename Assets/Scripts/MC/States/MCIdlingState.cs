using System;
using UnityEngine;

namespace TKM
{
    public class MCIdlingState : MCBaseState
    {
        string IDLE_ANIMATION_NAME = "Idle";
        public MCIdlingState(MCController _MCController) : base(_MCController)
        {

        }
        public override void Enter()
        {
            base.Enter();

            EnableBothAttack();
            _MCController.Animator.Play(IDLE_ANIMATION_NAME);
        }

        public override void Exit()
        {
            base.Exit();

            DisableBothAttack();
        }
    }
}
