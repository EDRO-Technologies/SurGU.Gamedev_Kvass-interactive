using System;
using System.Collections;
using Logic.Slots;
using UnityEngine;

namespace Enemies
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private SlotGrid _slotGrid;
        [SerializeField] private EnemySpawnPosition _enemySpawnPosition;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private float _minCooldown;
        [SerializeField] private float _maxCooldown;
        private const int MaxTurretLevelSum = 36;
        private Coroutine _spawnCoroutine;
        
        public event Action EnemyDied;

        public void StartSpawn() => _spawnCoroutine = StartCoroutine(SpawnCoroutine());

        public void StopSpawn() => StopCoroutine(_spawnCoroutine);

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                _enemySpawnPosition.UpdatePosition();
                var enemy = Instantiate(_enemyPrefab, _enemySpawnPosition.transform.position, Quaternion.identity);
                enemy.Health.Died += OnEnemyDeath;
                yield return new WaitForSeconds(GetSpawnCooldown());
            }
        }
        
        private float GetSpawnCooldown()
        {
            var turretsLevelSuminterpolated =
                Mathf.InverseLerp(0, MaxTurretLevelSum, _slotGrid.GetTurretsLevelSum());

            Debug.Log("Enemy factory sc " + Mathf.Lerp(_minCooldown, _maxCooldown, turretsLevelSuminterpolated));
            return Mathf.Lerp(_minCooldown, _maxCooldown, turretsLevelSuminterpolated);
        }

        private void OnEnemyDeath() => EnemyDied?.Invoke();
    }
}