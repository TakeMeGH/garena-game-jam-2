using UnityEngine;

namespace TKM
{
    public class EnemyIdentifier : MonoBehaviour
    {
        public EnemyType Type;
        public Transform TeleportPoint;

        public void OnHitEnemy()
        {
            if (gameObject.TryGetComponent<EnemyController>(out var enemyController))
            {
                enemyController.OnHitEnemy();
            }
        }
    }
}
