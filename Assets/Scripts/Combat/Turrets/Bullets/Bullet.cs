using System;
using UnityEngine;

namespace Combat.Turrets.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        private float _force;

        private int _damage;
        private Vector3 _direction;


        public void Construct(int damage, Vector3 direction, float force)
        {
            _damage = damage;
            _direction = direction;
            _force = force;
        }

        private void Start() =>
            Destroy(gameObject, 3f);

        private void FixedUpdate() =>
            _rigidbody.AddForce(_direction.normalized * _force, ForceMode.Impulse);
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.GetDamage(_damage);
                Destroy(gameObject);
            }
                
        }
    }
}