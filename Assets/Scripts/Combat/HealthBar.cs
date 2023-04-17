using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _progress;
        [SerializeField] private Wall _wall;

        private void OnEnable()
        {
            _wall.HealthChanged += OnHealthChange;
        }

        private void OnDisable()
        {
            _wall.HealthChanged -= OnHealthChange;
        }

        private void OnHealthChange(int health)
        {
            float onePercent = _wall.Health.MaxAmount / 100;

            
            var fillAmount = (health / onePercent) / 100;
            _progress.fillAmount = fillAmount;
        }
        
    }
}