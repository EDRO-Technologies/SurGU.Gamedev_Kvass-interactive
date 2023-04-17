using System;
using System.Collections;
using CartoonFX;
using Logic.Slots;
using UnityEngine;

namespace Fx
{
    public class SlotFX : MonoBehaviour
    {
        [SerializeField] private Slot _slot;
        [SerializeField] private CFXR_Effect _aura;
        [SerializeField] private float _auraTime;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Animator _auraAnimator;


        private void OnEnable()
        {
            _slot.ItemMerged += OnMerge;
            
        }

        private void OnDisable()
        {
            _slot.ItemMerged -= OnMerge;
        }

        private void OnMerge()
        {
            _audioSource.PlayOneShot(_audioClip);
            StartCoroutine(AuraCoroutine());
        }

        private IEnumerator AuraCoroutine()
        {
            _aura.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(_auraTime/2);
            _auraAnimator.SetTrigger("Hide");
            yield return new WaitForSeconds(_auraTime/2);
            _aura.gameObject.SetActive(false);
        }
    }
}