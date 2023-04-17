using System;
using System.Runtime.InteropServices;
using Combat.Turrets;
using Enemies;
using UnityEngine;
using Zenject;

namespace Logic.Slots
{
    public class EnemyLoots : MonoBehaviour
    {
        [SerializeField] private SlotGrid _slotGrid;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private TurretConfig _connanConfig;
        [SerializeField] private TurretConfig _mortarConfig;
        [SerializeField] private int _enemyToTurret;
        [SerializeField] private int _turretToMortar;
        
        private TurretFactory _turretFactory;
        private int _currentEnemyToTurret;
        private int _currentTurretToMortar;

        [Inject]
        private void Construct(TurretFactory turretFactory) => _turretFactory = turretFactory;

        private void Awake()
        {
            _currentEnemyToTurret = _enemyToTurret;
            _currentTurretToMortar = _turretToMortar;
        }

        private void OnEnable() => _enemyFactory.EnemyDied += OnEnemyDeath;

        private void OnDisable() => _enemyFactory.EnemyDied -= OnEnemyDeath;

        private void OnEnemyDeath()
        {
            _currentEnemyToTurret--;
            
            if (_currentEnemyToTurret <= 0)
            {
                _currentTurretToMortar--;
                if (_currentTurretToMortar <= 0)
                {
                    _slotGrid.AddTurret(_turretFactory.Get(_mortarConfig));
                    _currentTurretToMortar = _turretToMortar;
                }
                else
                {
                    _slotGrid.AddTurret(_turretFactory.Get(_connanConfig));
                    
                }
                _currentEnemyToTurret = _enemyToTurret;
            }
        }
    }
}