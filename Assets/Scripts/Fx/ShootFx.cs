using System;
using Combat.Turrets;
using UnityEngine;

namespace Fx
{
    [RequireComponent(typeof(AudioSource))]
    public class ShootFx : MonoBehaviour
    {
        private const string ShootAnimatorTrigger = "Shoot";
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private TurretGun _turretGun;
        [SerializeField] private Animator _animator;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _turretGun.Shooted += OnShoot;
        }

        private void OnShoot()
        {
            _audioSource.PlayOneShot(_audioClip);
            _animator.SetTrigger(ShootAnimatorTrigger);
        }
    }
}