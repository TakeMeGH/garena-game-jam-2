using System;
using System.Diagnostics;
using UnityEngine;

namespace TKM
{
    public class EnemyWalkingState : IState
    {
        EnemyController _enemyController;
        float _speedWithDirection;

        public EnemyWalkingState(EnemyController _enemyController)
        {
            this._enemyController = _enemyController;
        }
        public void Enter()
        {
            _enemyController.Animator.SetFloat("Speed", _enemyController.Speed);
            _speedWithDirection = _enemyController.Speed;
            if (CalculateDistance() < 0)
            {
                _speedWithDirection *= -1;
            }
        }

        public void Exit()
        {
        }

        public void PhysicsUpdate()
        {
        }

        public void Update()
        {
            _enemyController.Rigidbody.linearVelocity = new Vector2(_speedWithDirection, 0);
            if (Math.Abs(CalculateDistance()) < _enemyController.StopingDistance)
            {
                _enemyController.SwitchState(_enemyController.EnemyIdlingState);
            }
        }

        float CalculateDistance()
        {
            return _enemyController.TargetX - _enemyController.transform.position.x;
        }
    }
}
