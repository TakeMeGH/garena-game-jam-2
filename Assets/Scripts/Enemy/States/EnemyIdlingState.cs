using UnityEngine;

namespace TKM
{
    public class EnemyIdlingState : IState
    {
        EnemyController _enemyController;

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
        }

        public void PhysicsUpdate()
        {
        }

        public void Update()
        {
        }
    }
}
