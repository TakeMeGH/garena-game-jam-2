using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace TKM
{
    public class BloomFlashTogler : MonoBehaviour
    {
        private static BloomFlashTogler _instance;
        public static BloomFlashTogler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<BloomFlashTogler>();
                }
                return _instance;
            }
        }

        public Volume postProcessingVolume; // Reference to your Post-Processing Volume
        private Bloom bloom;

        public float maxIntensity = 10f; // Max intensity for the bloom effect
        public float animationDuration = 2f; // Duration of the effect

        private void Start()
        {
            // Get the Bloom component from the volume
            if (postProcessingVolume.profile.TryGet<Bloom>(out bloom))
            {
                bloom.intensity.value = 0f; // Set initial intensity
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                TriggerBloom();
            }
        }
        public void TriggerBloom()
        {
            StartCoroutine(AnimateBloomEffect());
        }

        private IEnumerator AnimateBloomEffect()
        {
            float elapsedTime = 0f;

            // Animate bloom intensity up
            while (elapsedTime < animationDuration / 2)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / (animationDuration / 2);
                bloom.intensity.value = Mathf.Lerp(0f, maxIntensity, t);
                yield return null;
            }

            elapsedTime = 0f;

            // Animate bloom intensity down
            while (elapsedTime < animationDuration / 2)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / (animationDuration / 2);
                bloom.intensity.value = Mathf.Lerp(maxIntensity, 0f, t);
                yield return null;
            }

            // Reset bloom intensity
            bloom.intensity.value = 0f;
        }
    }

}
