using UnityEngine;
using UnityEngine.EventSystems;

namespace TKM
{
    public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject pointerImg; 
        public void PlayButtonSound()
        {
            if (AudioManager.Instance != null) 
            {
                AudioManager.Instance.PlaySFX(SFX.button_click); 
            }
            else
            {
                Debug.LogWarning("AudioManager instance not found!");
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (pointerImg != null)
            {
                pointerImg.SetActive(true); 
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (pointerImg != null)
            {
                pointerImg.SetActive(false);
            }
        }
    }
}
