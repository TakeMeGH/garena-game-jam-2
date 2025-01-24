using UnityEngine;

namespace TKM
{
    public class MCBaseState : IState
    {
        protected MCController _MCController;
        public MCBaseState(MCController _MCController)
        {
            this._MCController = _MCController;
        }
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void PhysicsUpdate()
        {
        }

        public virtual void Update()
        {
            _MCController.LastInputTime += Time.deltaTime;
        }

        protected void EnableBothAttack()
        {
            _MCController.InputReader.LeftAttackPerformed += OnLeftAttackPerformed;
            _MCController.InputReader.RightAttackPerformed += OnRightAttackPerformed;
        }
        protected void DisableBothAttack()
        {
            _MCController.InputReader.LeftAttackPerformed -= OnLeftAttackPerformed;
            _MCController.InputReader.RightAttackPerformed -= OnRightAttackPerformed;
        }
        private void OnLeftAttackPerformed()
        {
            _MCController.SwitchState(_MCController.MCLeftAttackState);
        }
        private void OnRightAttackPerformed()
        {
            _MCController.SwitchState(_MCController.MCRightAttackState);
        }

    }
}
