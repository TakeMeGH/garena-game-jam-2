using UnityEngine;

namespace TKM
{
    public class EnemyIdentifier : MonoBehaviour
    {
        public EnemyType Type;
        public Transform TeleportPoint;

        public void OnHitEnemy()
        {
            if (this == null || gameObject == null) return;
            if (gameObject.TryGetComponent<EnemyController>(out var enemyController))
            {
                enemyController.OnHitEnemy();
            }
            else if (gameObject.TryGetComponent<IPerks>(out var perks))
            {
                perks.Activate();
            }
        }
    }
}
