using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class Wave : MonoBehaviour
    {
        [SerializeField] private WaveStartButton _waveStartButton;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private float _waveTime;
        private bool _waveStart;

        public int WaveCount { get; private set; }

        private void Awake()
        {
            WaveCount = 1;
            _waveStartButton.SetWaveCount(WaveCount);
        }

        private void OnEnable() => _waveStartButton.Clicked += StartWave;

        private void OnDisable() => _waveStartButton.Clicked -= StartWave;

        private void StartWave()
        {
            if (_waveStart == false)
            {
                _waveStartButton.gameObject.SetActive(false);
                _waveStart = true;
                _enemyFactory.StartSpawn();
                StartCoroutine(StopWaveCoroutine());
            }
        }

        private IEnumerator StopWaveCoroutine()
        {
            yield return new WaitForSeconds(_waveTime);
            StopWave();
        }

        private void StopWave()
        {
            _enemyFactory.StopSpawn();
            _waveStart = false;
            WaveCount++;
            _waveStartButton.SetWaveCount(WaveCount);
            _waveStartButton.gameObject.SetActive(true);
        }
    }
}