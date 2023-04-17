using UnityEngine;

namespace Combat.Turrets
{

    public class TurretRotation : MonoBehaviour
    {
        [SerializeField] private Turret _turret;
        [SerializeField] private TargetObserver _observer;
        private float _maxTurnSpeed;

        private Transform Target => _observer.CurrentTarget;

        private void Awake()
        {
            _maxTurnSpeed = _turret.TurretConfig.MaxTurnSpeed;
        }

        private void Update()
        {
            if (Target != null && _observer.HaveTarget )
                Aim();
        }

        private void Aim()
        {
            Vector3 direction = Target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation,
            rotation, _maxTurnSpeed * Time.deltaTime);
        }
    }
}