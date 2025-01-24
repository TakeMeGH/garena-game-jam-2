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
        }

        public void Exit()
        {
        }

        public void PhysicsUpdate()
        {
        }

        public void Update()
        {
        }
    }
}
