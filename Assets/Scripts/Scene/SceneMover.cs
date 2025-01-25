using UnityEngine;
using UnityEngine.SceneManagement;

namespace TKM
{
    public class SceneMover : MonoBehaviour
    {
        public void LoadNextScene(string nextScene)
        {
            SceneManager.LoadScene(nextScene);
        }

        public void RetryCurrentScene()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }

    }
}
