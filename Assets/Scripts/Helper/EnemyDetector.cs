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
    public class DetectorResult
    {
        public EnemyIdentifier EnemyIdentifier;
        public bool IsMiss;
        public Vector2 EnemyPosition;

        public DetectorResult(EnemyIdentifier enemyIdentifier, bool isMiss, Vector2 enemyPosition)
        {
            EnemyIdentifier = enemyIdentifier;
            IsMiss = isMiss;
            EnemyPosition = enemyPosition;
        }
    }
    public class EnemyDetector : MonoBehaviour
    {
        List<EnemyIdentifier> _enemies = new();
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<EnemyIdentifier>(out var enemy))
            {
                _enemies.Add(enemy);
            }
        }

        public DetectorResult GetNearestEnemyOfType(EnemyType type, Vector2 position)
        {
            EnemyIdentifier selectedEnemy = null;
            int index = -1;
            int selectedIndex = -1;
            foreach (EnemyIdentifier enemy in _enemies)
            {
                index++;
                if (selectedEnemy == null && enemy.Type == type)
                {
                    selectedEnemy = enemy;
                    selectedIndex = index;
                    continue;
                }

                if (enemy.Type == type)
                {
                    float distance = Math.Abs(selectedEnemy.transform.position.x - position.x);
                    float newDistance = Math.Abs(enemy.transform.position.x - position.x);
                    if (newDistance < distance)
                    {
                        selectedEnemy = enemy;
                        selectedIndex = index;
                    }
                }
            }
            if (selectedEnemy == null)
            {
                while (_enemies.Count > 0 && _enemies[0] == null)
                {
                    _enemies.RemoveAt(0);
                }
                if (_enemies.Count > 0)
                {
                    return new DetectorResult(_enemies[0], true, _enemies[0].TeleportPoint.position);
                }
                else
                {
                    return new DetectorResult(null, true, position);
                }
            }
            _enemies.RemoveAt(selectedIndex);
            return new DetectorResult(selectedEnemy, false, selectedEnemy.TeleportPoint.position);
        }
    }
}
