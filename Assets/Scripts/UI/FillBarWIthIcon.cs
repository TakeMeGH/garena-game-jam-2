using UnityEngine;
using UnityEngine.UI;

namespace TKM
{

    public class FillBarWithIcon : MonoBehaviour
    {
        public Image fillBar; // Reference to the fill bar image
        public RectTransform icon; // Reference to the icon RectTransform

        private RectTransform fillBarRect;

        int TotalAllKill = 0;

        void Start()
        {
            if (fillBar != null)
                fillBarRect = fillBar.GetComponent<RectTransform>();

            TotalAllKill = Spawner.Instance.GetAllNeededKill();
        }

        void Update()
        {
            if (fillBar != null && icon != null)
            {
                fillBar.fillAmount = (float)Spawner.Instance.TotalKillCount / TotalAllKill;
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
