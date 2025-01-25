using System;
using System.Diagnostics;
using UnityEngine;

namespace TKM
{
    public class EnemyWalkingState : IState
    {
        EnemyController _enemyController;
        float _speedWithDirection;

        #region Flying
        float _lerpValue;
        float _currentTime;
        Vector3 _defaultPostion;

        #endregion

        public EnemyWalkingState(EnemyController _enemyController)
        {
            this._enemyController = _enemyController;
        }
        public void Enter()
        {
            if (_enemyController.IsFlying == false)
            {
                _enemyController.Animator.SetFloat("Speed", _enemyController.Speed);
                _speedWithDirection = _enemyController.Speed;
                if (CalculateDistance() < 0)
                {
                    _speedWithDirection *= -1;
                }
            }
            else
            {
                _enemyController.Animator.Play("Glide");
                _defaultPostion = _enemyController.transform.position;
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
            if (_enemyController.IsFlying == false)
            {
                _enemyController.Rigidbody.linearVelocity = new Vector2(_speedWithDirection, 0);
                if (Math.Abs(CalculateDistance()) < _enemyController.StopingDistance)
                {
                    _enemyController.Rigidbody.linearVelocity = Vector2.zero;
                    _enemyController.SwitchState(_enemyController.EnemyIdlingState);
                }
            }
            else
            {
                _currentTime += Time.deltaTime;

                _lerpValue = Mathf.Min(1f, _currentTime / _enemyController.FlyTime);
                float newYLerpValue = Mathf.Min(1, (float)Math.Log10(1 + _lerpValue * (10 - 1)) * _enemyController.FlyMultiplier);

                float newX = Mathf.Lerp(_defaultPostion.x, _enemyController.TargetX, _lerpValue);
                float newY = Mathf.Lerp(_defaultPostion.y, _enemyController.TargetY, newYLerpValue);

                _enemyController.transform.position = new Vector2(newX, newY);

                if (newYLerpValue >= 1f)
                {
                    _enemyController.Animator.Play("Idle");
                }
                if (_lerpValue >= 1f)
                {
                    _enemyController.SwitchState(_enemyController.EnemyIdlingState);
                }
            }
        }

        float CalculateDistance()
        {
            return _enemyController.TargetX - _enemyController.transform.position.x;
        }
    }
}
