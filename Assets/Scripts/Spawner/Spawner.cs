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
        [SerializeField] PairRange PosXLeft;
        [SerializeField] PairRange PosXRight;
        [SerializeField] PairRange PosY;
        int _killCount = 0;
        int _waveIndex = -1;

        private void Start()
        {
            StartNextWave();
        }

        void StartNextWave()
        {
            _waveIndex++;
            if (_waveIndex == _waves.Count) return;

            _killCount = 0;
            StartCoroutine(ProceedWave());
        }
        IEnumerator ProceedWave()
        {
            for (int i = 0; i < _waves[_waveIndex].EnemyCount; i++)
            {
                yield return new WaitForSeconds(_waves[_waveIndex].SpawnSpeed);
                SpawnEnemy();
            }
        }

        public void IncreaseKillCount()
        {
            _killCount++;
            if (_killCount == _waves[_waveIndex].EnemyCount)
            {
                StartNextWave();
            }
        }

        void SpawnEnemy()
        {
            int randomEnemyIndex = UnityEngine.Random.Range(0, _enemies.Count);
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
    }
}
