using System;
using UnityEngine;

namespace TKM
{
    public class MCMissState : MCBaseState
    {
        private float _currentTime = 0;
        private Vector3 _originalPosition;
        private float _shakeIntensity = 0.1f;
        private float _shakeFrequency = 10f;
        private bool _isShaking = false;

        public MCMissState(MCController _MCController) : base(_MCController)
        {
        }

        public override void Enter()
        {

            _originalPosition = _MCController.transform.position;
            _isShaking = true;
        }

        public override void Update()
        {

            _currentTime += Time.deltaTime;

            if (_isShaking)
            {
                ApplyShakeEffect();
            }

            if (_currentTime >= _MCController.MissWaitTime)
            {
                OnWaitTimeFinished();
            }
        }

        public override void Exit()
        {
            base.Exit();

            _MCController.transform.position = _originalPosition;
            _isShaking = false;
            _currentTime = 0;
        }

        private void ApplyShakeEffect()
        {
            float offsetX = Mathf.Sin(Time.time * _shakeFrequency) * _shakeIntensity;
            float offsetY = Mathf.Cos(Time.time * _shakeFrequency) * _shakeIntensity;

            _MCController.transform.position = _originalPosition + new Vector3(offsetX, offsetY, 0);
        }

        public void OnWaitTimeFinished()
        {
            _MCController.SwitchState(_MCController.MCIdlingState);
        }
    }
}
