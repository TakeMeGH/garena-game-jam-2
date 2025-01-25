using UnityEngine;

namespace TKM
{
    public class EnemyAttackingState : IState
    {
        EnemyController _enemyController;

        public EnemyAttackingState(EnemyController _enemyController)
        {
            this._enemyController = _enemyController;
        }
        public void Enter()
        {
            _enemyController.Animator.Play("Attack");

            _enemyController.OnAnimationFinished += OnAnimationFinished;
            _enemyController.EnableNextInput += OnEnableNextInput;

        }

        public void Exit()
        {
            _enemyController.OnAnimationFinished -= OnAnimationFinished;
            _enemyController.EnableNextInput -= OnEnableNextInput;

        }

        public void PhysicsUpdate()
        {
        }

        public void Update()
        {
        }

        public void OnAnimationFinished()
        {
            _enemyController.SwitchState(_enemyController.EnemyDeadState);
        }

        public void OnEnableNextInput()
        {
            Crystal.Instance.Attacked();
        }

    }
}
