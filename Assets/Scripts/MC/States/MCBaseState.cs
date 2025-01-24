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

        protected void OnLeftAttackPerformed()
        {
            ProcessDetectorResult(_MCController.LeftBound.GetNearestEnemyOfType(0, _MCController.DefaultPosition));
            _MCController.NextAttackAnimation = _MCController.LEFT_GROUND_ATTACK_ANIMATION_NAME;
            _MCController.NextAttackFacing = -1;

            _MCController.SwitchState(_MCController.MCAttackState);
        }
        protected void OnRightAttackPerformed()
        {
            ProcessDetectorResult(_MCController.RightBound.GetNearestEnemyOfType(_MCController.Type, _MCController.DefaultPosition));
            _MCController.NextAttackAnimation = _MCController.RIGHT_GROUND_ATTACK_ANIMATION_NAME;
            _MCController.NextAttackFacing = 1;

            _MCController.SwitchState(_MCController.MCAttackState);
        }

        void ProcessDetectorResult(DetectorResult detectorResult)
        {
            _MCController.NextPosition = detectorResult.EnemyPosition;
            _MCController.IsAttackMiss = detectorResult.IsMiss;
            _MCController.NextEnemy = detectorResult.EnemyIdentifier;
        }

        protected void ProceedInput()
        {
            if (_MCController.LastInput != 0 && _MCController.LastInputTime <= _MCController.BufferInputTIme)
            {
                switch (_MCController.LastInput)
                {
                    case 1:
                        OnRightAttackPerformed();
                        break;
                    case -1:
                        OnLeftAttackPerformed();
                        break;
                }
                _MCController.ResetInput();
            }
        }
    }
}
