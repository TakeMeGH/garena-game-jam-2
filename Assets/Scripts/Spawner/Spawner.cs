using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TKM
{
    [Serializable]
    public class PairRange
    {
        public float Left;
        public float Right;
    }
    [Serializable]
    public class Wave
    {
        public int EnemyCount;
        public float SpawnSpeed;
        public float IdleTime;
    }

    [Serializable]
    public class EnemyData
    {
        public GameObject PrefabEnemy;
        public Transform EnemyTransform;
        public float Weight;
    }

    public class Spawner : MonoBehaviour
    {
        private static Spawner _instance;
        public static Spawner Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<Spawner>();
                }
                return _instance;
            }
        }
        [SerializeField] List<Wave> _waves = new();
        [SerializeField] List<EnemyData> _enemies;
        [SerializeField] List<EnemyData> _perks;
        [SerializeField] PairRange PosXLeft;
        [SerializeField] PairRange PosXRight;
        [SerializeField] PairRange PosY;
        [Header("Event")]
        [SerializeField] VoidEvent _onWinConditions;
        int _killCount = 0;
        int _waveIndex = -1;
        public int TotalKillCount = 0;

        private void Start()
        {
            StartNextWave();
        }

        void StartNextWave()
        {
            _waveIndex++;
            if (_waveIndex == _waves.Count)
            {
                _onWinConditions.RaiseEvent();
                return;
            }

            _killCount = 0;
            StartCoroutine(ProceedWave());
        }
        IEnumerator ProceedWave()
        {
            for (int i = 0; i < _waves[_waveIndex].EnemyCount; i++)
            {
                yield return new WaitForSeconds(_waves[_waveIndex].SpawnSpeed);
                SpawnEnemy();
                SpawnPerks();
            }
        }

        public void IncreaseKillCount()
        {
            _killCount++;
            TotalKillCount++;
            if (_killCount == _waves[_waveIndex].EnemyCount)
            {
                StartNextWave();
            }
        }

        void SpawnEnemy()
        {
            float totalValue = 0;
            for (int i = 0; i < _enemies.Count; i++) totalValue += _enemies[i].Weight;

            int randomEnemyIndex = 0;
            float randomValue = UnityEngine.Random.Range(0.0001f, totalValue);
            float valueBefore = 0f;
            for (int i = 0; i < _enemies.Count; i++)
            {
                float nextValue = valueBefore + _enemies[i].Weight;
                if (randomValue > valueBefore && randomValue <= nextValue)
                {
                    randomEnemyIndex = i;
                    break;
                }
                valueBefore = nextValue;
            }

            GameObject spawnedEnemy = Instantiate(_enemies[randomEnemyIndex].PrefabEnemy, _enemies[randomEnemyIndex].EnemyTransform.position, _enemies[randomEnemyIndex].EnemyTransform.rotation);
            Vector3 eulerAngles = spawnedEnemy.transform.eulerAngles;

            float randomPosX = UnityEngine.Random.Range(PosXLeft.Left, PosXLeft.Right);

            if (Mathf.Approximately(eulerAngles.y, 180f))
            {
                randomPosX = UnityEngine.Random.Range(PosXRight.Left, PosXRight.Right);
            }

            float randomPosY = UnityEngine.Random.Range(PosY.Left, PosY.Right);
            spawnedEnemy.GetComponent<EnemyController>().SetRandomData(randomPosX, randomPosY, _waves[_waveIndex].IdleTime);
        }
        private void SpawnPerks()
        {
            for (int i = 0; i < _perks.Count; i++)
            {
                float randomValue = UnityEngine.Random.Range(0f, 1f);
                if (randomValue <= _perks[i].Weight)
                {
                    Instantiate(_perks[i].PrefabEnemy, _perks[i].EnemyTransform.position, _perks[i].EnemyTransform.rotation);
                    if (i % 2 == 0) i++;
                }
            }
        }

        public int GetAllNeededKill()
        {
            int total = 0;
            foreach (Wave wave in _waves)
            {
                total += wave.EnemyCount;
            }
            return total;
        }
    }
}
