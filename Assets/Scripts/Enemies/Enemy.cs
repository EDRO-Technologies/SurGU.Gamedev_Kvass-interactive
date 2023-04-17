using System.Collections;
using Combat;
using Combat.Turrets;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class Enemy : MonoBehaviour, IDamageable, ITurretTarget
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _damage;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _stanSpeed;
        [SerializeField] private float _stanTime;
        private readonly Vector3 MoveDirection = Vector3.back;
        private Rigidbody _rigidbody;
        private Coroutine _moveCoroutine;
        private bool _stan;

        public Health Health { get; private set; }
        
        private Vector3 MoveVelocity => MoveDirection * _moveSpeed;
        private Vector3 StanVelocity => -MoveDirection * _stanSpeed;

        private void Awake()
        {
            Health = new Health(_maxHealth);
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            Health.Died += Death;
        }

        private void OnDisable()
        {
            Health.Died -= Death;
        }

        private void Start()
        {
            _stan = false;
            _rigidbody.velocity = MoveVelocity;
            // _moveCoroutine = StartCoroutine(MoveCoroutine());
        }

        public void GetDamage(int damage)
        {
            Health.TakeDamage(damage);
            if (_stan == false)
            {
                StartCoroutine(StanCoroutine());
            }
        }

        private void Death()
        {
            Destroy(gameObject);
        }

        private IEnumerator StanCoroutine()
        {
            _rigidbody.velocity = StanVelocity;
            _stan = true;
            yield return new WaitForSeconds(_stanTime);
            _rigidbody.velocity = MoveVelocity;
            _stan = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Wall wall))
            {
                wall.GetDamage(_damage);
                Destroy(gameObject);
            }
        }

        // private IEnumerator MoveCoroutine()
        // {
        //     while (true)
        //     {
        //         // Debug.Log("Coroutine");
        //         // _rigidbody.AddForce(Vector3.back * _moveSpeed);
        //         // if (_rigidbody.velocity.magnitude > _moveSpeed)
        //         //     _rigidbody.velocity = _rigidbody.velocity.normalized * _moveSpeed;
        //         
        //         
        //         
        //         // Debug.Log("Magnitude" + _rigidbody2D.velocity.magnitude);
        //         
        //         yield return new WaitForFixedUpdate();
        //     }
        // }
    }
}