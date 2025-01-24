using UnityEngine;
using UnityEngine.Events;

namespace TKM
{
    public class MCController : StateMachine
    {
        [field: Header("Input Reader")]
        [field: SerializeField] public InputReader InputReader;
        #region Component

        [field: Header("Component")]
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        #endregion

        #region SharedData
        public bool IsNextInputEnabled { get; private set; } = true;
        public UnityEvent OnAnimationFinished;
        #endregion

        #region State
        public MCIdlingState MCIdlingState { get; private set; }
        public MCLeftAttackState MCLeftAttackState { get; private set; }
        public MCRightAttackState MCRightAttackState { get; private set; }
        #endregion
        void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            MCIdlingState = new MCIdlingState(this);
            MCLeftAttackState = new MCLeftAttackState(this);
            MCRightAttackState = new MCRightAttackState(this);
        }
        void Start()
        {
            Initialize();
            SwitchState(MCIdlingState);
        }

        public void SetIsNextInputEnabled(bool status)
        {
            IsNextInputEnabled = status;
        }

        public void TriggerAnimationFinished()
        {
            OnAnimationFinished.Invoke();
        }
    }
}
