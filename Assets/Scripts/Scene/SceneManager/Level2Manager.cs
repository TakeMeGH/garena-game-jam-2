using UnityEngine;

namespace TKM
{
    public class Level2Manager : MonoBehaviour
    {
        [SerializeField] InputReader _inputReader;

        void Start()
        {
            _inputReader.EnableGameplayInput();
            AudioManager.Instance.PlayBGM(BGM.Level_2);
        }
    }
}
