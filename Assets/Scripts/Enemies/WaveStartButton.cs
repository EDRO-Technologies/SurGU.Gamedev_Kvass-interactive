using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class WaveStartButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _waveCountText;
        [SerializeField] private Button _button;

        public event Action Clicked;

        public void SetWaveCount(int waveCount)
        {
            _waveCountText.text = $"Волна {waveCount}";
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Clicked?.Invoke();
        }
    }
}