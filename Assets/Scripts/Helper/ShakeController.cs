using Unity.Cinemachine;
using UnityEngine;

namespace TKM
{
    public class ShakeController : MonoBehaviour
    {
        private static ShakeController _instance;
        public static ShakeController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<ShakeController>();
                }
                return _instance;
            }
        }

        [SerializeField] float _amplititudeGain;
        [SerializeField] float _frequencyGain;
        [SerializeField] CinemachineBasicMultiChannelPerlin _noise;
        [SerializeField] float _duration;
        float _lastTime;

        void Update()
        {
            _lastTime += Time.deltaTime;
            if (_lastTime >= _duration)
            {
                _noise.AmplitudeGain = 0;
                _noise.FrequencyGain = 0;
            }
        }

        public void ToggleShake()
        {
            _lastTime = 0;
            _noise.AmplitudeGain = _amplititudeGain;
            _noise.FrequencyGain = _frequencyGain;
        }
    }
}
