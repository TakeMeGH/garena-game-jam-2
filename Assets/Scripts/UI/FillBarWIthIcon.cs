using UnityEngine;
using UnityEngine.UI;

namespace TKM
{

    public class FillBarWithIcon : MonoBehaviour
    {
        public Image fillBar; // Reference to the fill bar image
        public RectTransform icon; // Reference to the icon RectTransform

        private RectTransform fillBarRect;

        void Start()
        {
            if (fillBar != null)
                fillBarRect = fillBar.GetComponent<RectTransform>();
        }

        void Update()
        {
            if (fillBar != null && icon != null)
            {
                // Get the width of the fill bar
                float barWidth = fillBarRect.rect.width;

                // Calculate the icon's position based on the fill amount
                float fillAmount = fillBar.fillAmount; // 0 to 1
                float iconXPosition = -fillAmount * barWidth + (barWidth / 2);

                // Update the icon's position
                icon.anchoredPosition = new Vector2(iconXPosition, icon.anchoredPosition.y);
            }
        }
    }   
}
