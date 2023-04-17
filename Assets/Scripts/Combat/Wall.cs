using System;
using System.Collections;
using UnityEngine;

namespace Combat
{
    public class Wall : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _healSpeed;
        public Health Health { get; private set; }

        public event Action<int> HealthChanged;

        private void OnEnable() => Health.HealthChanged += OnHealthChange;
        
        private void OnDisable() => Health.HealthChanged -= OnHealthChange;

        private void Awake()
        {
            Health = new Health(_maxHealth);
        }

        private void Start()
        {
            StartCoroutine(HealthCoroutine());
        }

        private void OnHealthChange(int health) => HealthChanged?.Invoke(health);

        public void GetDamage(int damage)
        {
            Health.TakeDamage(damage);
        }

        private IEnumerator HealthCoroutine()
        {
            while (true)
            {
                Health.Heal(1);
                yield return new WaitForSeconds(_healSpeed);
            }
        }
    }
}