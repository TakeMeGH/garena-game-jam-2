using System;
using UnityEngine;

namespace TKM
{
    public class EnemyController : StateMachine
    {
        #region Preset Input Data
        [field: Header("Preset Input Data")]
        public float Speed;
        public float StopingDistance;
        public float TargetX;
        public float IdleWaitTime = 3f;
        #endregion

        #region Flying Data
        [field: Header("Flying Data")]
        public bool IsFlying = false;
        public float FlyTime = 6;
        public float FlyMultiplier = 1.25f;
        public float TargetY;
        #endregion
        #region Component
        [field: Header("Component")]
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        #endregion

        #region SharedData
        public Action OnAnimationFinished;
        public Action EnableNextInput;
        #endregion
        #region State
        public EnemyIdlingState EnemyIdlingState { get; private set; }
        public EnemyWalkingState EnemyWalkingState { get; private set; }
        public EnemyAttackingState EnemyAttackingState { get; private set; }
        public EnemyDeadState EnemyDeadState { get; private set; }

        #endregion


        void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            EnemyIdlingState = new EnemyIdlingState(this);
            EnemyWalkingState = new EnemyWalkingState(this);
            EnemyAttackingState = new EnemyAttackingState(this);
            EnemyDeadState = new EnemyDeadState(this);
        }
        void Start()
        {
            Initialize();
            SwitchState(EnemyWalkingState);
        }

        public void Dead()
        {
            Destroy(gameObject);
        }

        public void OnHitEnemy()
        {
            SwitchState(EnemyDeadState);
        }

        public void TriggerEnableNextInput()
        {
            EnableNextInput?.Invoke();
        }

        public void TriggerAnimationFinished()
        {
            OnAnimationFinished?.Invoke();
        }

        public void SetRandomData(float posX, float posY, float IdleTime)
        {
            TargetX = posX;
            TargetY = posY;
            IdleWaitTime = IdleTime;
        }

    }
}
