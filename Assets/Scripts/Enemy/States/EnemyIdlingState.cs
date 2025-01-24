using UnityEngine;

namespace TKM
{
    public class EnemyIdlingState : IState
    {
        EnemyController _enemyController;
        float _currentTime;

        public EnemyIdlingState(EnemyController _enemyController)
        {
            this._enemyController = _enemyController;
        }
        public void Enter()
        {
            _enemyController.Animator.SetFloat("Speed", 0f);
        }

        public void Exit()
        {
            _currentTime = 0;
        }

        public void PhysicsUpdate()
        {
        }

        public void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _enemyController.IdleWaitTime)
            {
                _enemyController.SwitchState(_enemyController.EnemyAttackingState);
            }
        }
    }
}
