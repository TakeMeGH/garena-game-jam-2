using System.Collections;
using UnityEngine;

namespace TKM
{
    public class MCBaseState : IState
    {
        protected MCController _MCController;
        [SerializeField] Material blurMaterial;
        string blurProperty = "_BlurAmmount";
        float _targetBlurAmount = 0;
        float _duration = 0.25f;
        float _initialBlurAmount = 0.3f;
        float _elapsedTime = 0f;
        private bool _isTransitioning = false;
        private float _currentBlurAmount;


        public MCBaseState(MCController _MCController)
        {
            this._MCController = _MCController;
        }
        public virtual void Enter()
        {
            _MCController.StunAnimator.Play("Idle");
            blurMaterial = _MCController.SpriteRenderer.material;
            blurMaterial.SetFloat(blurProperty, _initialBlurAmount);
            _isTransitioning = true;
            _elapsedTime = 0f;

        }

        public virtual void Exit()
        {
        }

        public virtual void PhysicsUpdate()
        {
        }

        public virtual void Update()
        {
            if (_isTransitioning)
            {

                _MCController.LastInputTime += Time.deltaTime;

                _elapsedTime += Time.deltaTime;

                // Calculate the interpolated blur value
                _currentBlurAmount = Mathf.Lerp(_initialBlurAmount, _targetBlurAmount, _elapsedTime / _duration);

                // Apply the interpolated value to the material
                blurMaterial.SetFloat(blurProperty, _currentBlurAmount);

                // End the transition when the duration is reached
                if (_elapsedTime >= _duration)
                {
                    blurMaterial.SetFloat(blurProperty, _targetBlurAmount);
                    _isTransitioning = false; // Stop further updates
                }
            }
        }

        protected void OnLeftAttackPerformed()
        {
            ProcessDetectorResult(_MCController.LeftBound.GetNearestEnemyOfType(_MCController.Type, _MCController.DefaultPosition));
            if (_MCController.DefaultPosition.y < _MCController.NextPosition?.y)
            {
                _MCController.NextAttackAnimation = _MCController.UP_ATTACK_ANIMATION_NAME;
            }
            else
            {
                _MCController.NextAttackAnimation = _MCController.DOWN_ATTACK_ANIMATION_NAME;
            }
            _MCController.NextAttackFacing = -1;

            _MCController.SwitchState(_MCController.MCAttackState);
        }
        protected void OnRightAttackPerformed()
        {
            ProcessDetectorResult(_MCController.RightBound.GetNearestEnemyOfType(_MCController.Type, _MCController.DefaultPosition));
            if (_MCController.DefaultPosition.y < _MCController.NextPosition?.y)
            {
                _MCController.NextAttackAnimation = _MCController.UP_ATTACK_ANIMATION_NAME;
            }
            else
            {
                _MCController.NextAttackAnimation = _MCController.DOWN_ATTACK_ANIMATION_NAME;
            }
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
