using System;
using Combat.Turrets.Bullets;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Combat.Turrets
{
    [RequireComponent(typeof(BulletFactory))]
    public class TurretGun : MonoBehaviour
    {
        [SerializeField] private Turret _turret;
        [SerializeField] private TargetObserver _turretObserver;
        [SerializeField] private Transform _shootDirection;
        
        private BulletFactory _bulletFactory;
        private float _reloadTime;
        private Bullet _bulletPrefab;
        private float _reloadTimer;

        public event Action Shooted; 


        private bool isReloaded => _reloadTimer < 0;

        private void Awake()
        {
            var turretConfig = _turret.TurretConfig;

            float reloadTimeAsync = Random.Range(0, 1.5f);
            _reloadTime = turretConfig.ReloadTime + reloadTimeAsync;
            _bulletPrefab = turretConfig.BulletPrefab;

            _bulletFactory = GetComponent<BulletFactory>();
        }

        private void Update()
        {
            _reloadTimer -= Time.deltaTime;

            if (_turretObserver.HaveTarget && isReloaded)
            {
                Shoot();
                _reloadTimer = _reloadTime;
            }
        }

        private void Shoot()
        {
            Shooted?.Invoke();
            Vector3 direction = _shootDirection.position - transform.position;
            _bulletFactory.CreateBullet(_bulletPrefab, direction);
        }
    }
}