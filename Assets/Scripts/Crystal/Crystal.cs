using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

namespace TKM
{
    public class Crystal : MonoBehaviour
    {
        private static Crystal _instance;
        float _lastAttackTime;
        readonly float INVULNERABLE_TIME = 1f;
        [SerializeField] VoidEvent _onLoseConditions;
        [Header("Icons")]
        [SerializeField] UnityEngine.UI.Image[] _icons;
        [SerializeField] Sprite _loseHealthIcons;
        [Header("Icons")]
        [SerializeField] Sprite[] _spoleStateImages;
        [SerializeField] SpriteRenderer _spoleRenderer;

        public static Crystal Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<Crystal>();
                }
                return _instance;
            }
        }


        public int Health;
        private void Update()
        {
            _lastAttackTime += Time.deltaTime;
        }
        public void Attacked()
        {
            if (_lastAttackTime > INVULNERABLE_TIME)
            {
                Health--;
                AudioManager.Instance.PlaySFX(SFX.Base_attacked);
                if (Health <= 0)
                {
                    _onLoseConditions.RaiseEvent();
                    return;
                }
                _lastAttackTime = 0;
                GameObject popThreadObject = new GameObject("PopThread");
                PopThread popThread = popThreadObject.AddComponent<PopThread>();
                popThread.Activate();

                _spoleRenderer.sprite = _spoleStateImages[Health - 1];
                _icons[Health].sprite = _loseHealthIcons;
            }
        }
    }
}
