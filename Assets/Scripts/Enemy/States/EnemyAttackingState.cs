using UnityEngine;

namespace TKM
{
    public class EnemyAttackingState : MonoBehaviour
    {
        EnemyController _enemyController;

        public EnemyAttackingState(EnemyController _enemyController)
        {
            this._enemyController = _enemyController;
        }
        public void Enter()
        {
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
