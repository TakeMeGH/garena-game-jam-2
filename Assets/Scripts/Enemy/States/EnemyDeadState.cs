using UnityEngine;

namespace TKM
{
    public class EnemyDeadState : IState
    {
        EnemyController _enemyController;

        public EnemyDeadState(EnemyController _enemyController)
        {
            this._enemyController = _enemyController;
        }
        public void Enter()
        {
            if (_enemyController.GetComponent<EnemyIdentifier>().Type != EnemyType.Bomb)
            {
                Spawner.Instance.IncreaseKillCount();
            }
            VFXSpawner.Instance.PlayDeadVFX(_enemyController.transform.position);
            _enemyController.Dead();
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
