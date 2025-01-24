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
        public EnemyType Type;
        #endregion

        #region Component
        [field: Header("Component")]
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        #endregion

        #region State
        public EnemyIdlingState EnemyIdlingState { get; private set; }
        public EnemyWalkingState EnemyWalkingState { get; private set; }
        public EnemyAttackingState EnemyAttackingState { get; private set; }
        #endregion


        void Initialize()
        {
            // DefaultPosition = transform.position;
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            EnemyIdlingState = new EnemyIdlingState(this);
            EnemyWalkingState = new EnemyWalkingState(this);
            EnemyAttackingState = new EnemyAttackingState(this);
        }
        void Start()
        {
            Initialize();
            SwitchState(EnemyWalkingState);
        }

    }
}
