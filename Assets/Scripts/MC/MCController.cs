using UnityEngine;

namespace TKM
{
    public class MCController : StateMachine
    {
        [field: Header("Input Reader")]
        [field: SerializeField] public InputReader InputReader;
        #region Component

        [field: Header("Component")]
        [field: SerializeField] public MCGroundChecker GroundDetector;
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        #endregion

        #region SharedData
        [field: Header("Read Only")]
        public Vector2 RawDirection { get; private set; }
        public MCMovementData MovementData;
        public MCJumpData JumpData;
        #endregion

        #region State
        public MCMovementState MCMovementState { get; private set; }
        public MCMovement MCMovement { get; private set; }
        public MCJump MCJump { get; private set; }
        #endregion

        void OnEnable()
        {
            InputReader.MoveEvent += MoveMC;
        }

        void OnDisable()
        {
            InputReader.MoveEvent -= MoveMC;

        }

        void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            MCMovementState = new MCMovementState(this);
            MCMovement = new MCMovement(this, MovementData);
            MCJump = new MCJump(this, JumpData);
        }
        void Start()
        {
            Initialize();
            SwitchState(MCMovementState);
        }

        void MoveMC(Vector2 pos)
        {
            RawDirection = pos.normalized;
        }

        public Vector3 GetMoveDirection()
        {
            Vector3 horizontal = transform.right * RawDirection.x;
            Vector3 vertical = transform.forward * RawDirection.y;
            return horizontal + vertical;
        }
    }
}
