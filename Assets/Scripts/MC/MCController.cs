using System;
using UnityEngine;
using UnityEngine.Events;

namespace TKM
{
    public enum InputType
    {
        Keyboard,
        Mouse
    }
    public class MCController : StateMachine
    {
        [field: Header("Input Reader")]
        [field: SerializeField] public InputReader InputReader;
        [field: SerializeField] public InputType InputType;
        #region Component

        [field: Header("Component")]
        public EnemyDetector LeftBound;
        public EnemyDetector RightBound;
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        #endregion

        #region Preset Input Data
        [field: Header("Preset Input Data")]
        public float BufferInputTIme = 0.2f;
        public EnemyType Type;
        public float MissWaitTime = 2f;
        public Vector2 DefaultPosition { get; private set; }
        public string DOWN_ATTACK_ANIMATION_NAME { get; private set; } = "AttackDown";
        public string UP_ATTACK_ANIMATION_NAME { get; private set; } = "AttackUp";

        #endregion

        #region SharedData
        [field: Header("Shared Data (ReadOnly)")]
        public int LastInput;
        public float LastInputTime;
        public bool IsNextInputEnabled { get; private set; } = true;

        #region Attack Info
        public string NextAttackAnimation;
        public int NextAttackFacing;
        public Vector2? NextPosition;
        public EnemyIdentifier NextEnemy;
        public bool IsAttackMiss;
        #endregion
        public Action OnAnimationFinished;
        public Action EnableNextInput;
        #endregion

        #region State
        public MCIdlingState MCIdlingState { get; private set; }
        public MCAttackState MCAttackState { get; private set; }
        public MCMissState MCMissState { get; private set; }
        #endregion

        private void OnEnable()
        {
            InputReader.LeftAttackPerformed[(int)InputType] += OnLeftAttackPerformed;
            InputReader.RightAttackPerformed[(int)InputType] += OnRightAttackPerformed;
        }

        private void OnDisable()
        {
            InputReader.LeftAttackPerformed[(int)InputType] -= OnLeftAttackPerformed;
            InputReader.RightAttackPerformed[(int)InputType] -= OnRightAttackPerformed;
        }
        void Initialize()
        {
            DefaultPosition = transform.position;

            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            MCIdlingState = new MCIdlingState(this);
            MCAttackState = new MCAttackState(this);
            MCMissState = new MCMissState(this);
        }
        void Start()
        {
            Initialize();
            SwitchState(MCIdlingState);
        }

        public void TriggerEnableNextInput()
        {
            EnableNextInput?.Invoke();
        }

        public void TriggerAnimationFinished()
        {
            OnAnimationFinished?.Invoke();
        }

        public void ResetInput()
        {
            LastInput = 0;
            LastInputTime = 10f; // large time
        }

        private void OnLeftAttackPerformed()
        {
            LastInput = -1;
            LastInputTime = 0;
        }

        private void OnRightAttackPerformed()
        {
            LastInput = 1;
            LastInputTime = 0;
        }
    }
}
