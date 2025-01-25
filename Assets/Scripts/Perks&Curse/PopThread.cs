using UnityEngine;

namespace TKM
{
    public class PopThread : MonoBehaviour
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
            EnemyController[] enemyControllers = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
            foreach (EnemyController enemyController in enemyControllers)
            {
                if (enemyController != null)
                {
                    enemyController.OnHitEnemy();
                }
            }
        }
    }
}
