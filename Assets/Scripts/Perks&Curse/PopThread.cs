using UnityEngine;

namespace TKM
{
    public class PopThread : MonoBehaviour, IPerks
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F11))
            {
                Activate();
            }
        }
        public void Activate()
        {
            AudioManager.Instance.PlaySFX(SFX.PopThread);
            EnemyController[] enemyControllers = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
            foreach (EnemyController enemyController in enemyControllers)
            {
                if (enemyController != null)
                {
                    enemyController.OnHitEnemy();
                }
            }
            Destroy(gameObject);
        }
    }
}
