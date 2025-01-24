using System;
using System.Collections.Generic;
using UnityEngine;

namespace TKM
{
    public enum EnemyType
    {
        White,
        Black
    }
    public class EnemyDetector : MonoBehaviour
    {
        List<EnemyIdentifier> _enemies = new();
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<EnemyIdentifier>(out var enemy))
            {
                Debug.Log("TRIGGER");
                _enemies.Add(enemy);
            }
        }

        public Vector2? GetNearestEnemyOfType(EnemyType type, Vector2 position)
        {
            EnemyIdentifier selectedEnemy = null;
            foreach (EnemyIdentifier enemy in _enemies)
            {
                if (selectedEnemy == null && enemy.Type == type)
                {
                    selectedEnemy = enemy;
                    continue;
                }

                if (enemy.Type == type)
                {
                    float distance = Math.Abs(selectedEnemy.transform.position.x - position.x);
                    float newDistance = Math.Abs(enemy.transform.position.x - position.x);
                    if (newDistance < distance)
                    {
                        selectedEnemy = enemy;
                    }
                }
            }
            if (selectedEnemy == null) return null;
            return selectedEnemy.TeleportPoint.position;
        }
    }
}
