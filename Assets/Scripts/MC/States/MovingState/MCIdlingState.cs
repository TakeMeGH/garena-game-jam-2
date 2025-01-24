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

            _MCController.Animator.Play(IDLE_ANIMATION_NAME);
            _MCController.transform.position = _MCController.DefaultPosition;
        }

        public override void Update()
        {
            base.Update();

            ProceedInput();

        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
