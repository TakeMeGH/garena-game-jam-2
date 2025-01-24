using System;
using UnityEngine;

namespace TKM
{
    public class MCMissState : MCBaseState
    {
        float _currentTime = 0;
        public MCMissState(MCController _MCController) : base(_MCController)
        {
        }

        public override void Enter()
        {
            base.Enter();

        }

        public override void Update()
        {
            base.Update();

            _currentTime += Time.deltaTime;
            if (_currentTime >= _MCController.MissWaitTime)
            {
                OnWaitTimeFinished();
            }
        }

        public override void Exit()
        {
            base.Exit();

            _currentTime = 0;
        }

        public void OnWaitTimeFinished()
        {
            _MCController.SwitchState(_MCController.MCIdlingState);
        }
    }
}
