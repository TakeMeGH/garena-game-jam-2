using UnityEngine;
using UnityEngine.Events;

namespace TKM
{
    public class UIInputEvent : MonoBehaviour
    {
        [SerializeField] InputReader _inputReader;
        CanvasGroup _canvasGroup;
        public UnityEvent MouseA;
        public UnityEvent MouseB;
        public UnityEvent KeyboardA;
        public UnityEvent KeyboardB;

        void OnEnable()
        {
            _inputReader.MouseAPerformed += OnMouseA;
            _inputReader.MouseBPerformed += OnMouseB;
            _inputReader.KeyboardAPerformed += OnKeyboardA;
            _inputReader.KeyboardBPerformed += OnKeyboardB;
        }

        void OnDisable()
        {
            _inputReader.MouseAPerformed -= OnMouseA;
            _inputReader.MouseBPerformed -= OnMouseB;
            _inputReader.KeyboardAPerformed -= OnKeyboardA;
            _inputReader.KeyboardBPerformed -= OnKeyboardB;
        }

        void Start()
        {
            _canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }

        void OnMouseA()
        {
            if (_canvasGroup.alpha == 0) return;
            MouseA?.Invoke();
        }

        void OnMouseB()
        {
            if (_canvasGroup.alpha == 0) return;
            MouseB?.Invoke();
        }

        void OnKeyboardA()
        {
            if (_canvasGroup.alpha == 0) return;
            KeyboardA?.Invoke();
        }

        void OnKeyboardB()
        {
            if (_canvasGroup.alpha == 0) return;
            KeyboardB?.Invoke();
        }
    }
}
