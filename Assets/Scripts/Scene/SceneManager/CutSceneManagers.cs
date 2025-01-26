using UnityEngine;

namespace TKM
{
    public class CutSceneManagers : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.Instance.PlayBGM(BGM.MainMenu);    
        }
    }
}
