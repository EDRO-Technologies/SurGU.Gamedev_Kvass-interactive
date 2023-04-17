using System;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Turrets
{
    [RequireComponent(typeof(Collider))]
    public class TargetObserver : MonoBehaviour
    {
        public bool HaveTarget { private set; get; }
        public Transform CurrentTarget { private set; get; }

        [SerializeField] private List<Transform> _targets;
        private int _targetIndex;

        private void Awake()
        {
            _targets = new List<Transform>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsTarget(other))
            {
                // HaveTarget = true;
                _targets.Add(other.transform);
            }
        }
        
        private void UpdateTarget()
        {
            try
            {
                foreach (var target in _targets)
                {
                    if (target != null)
                    {
                        HaveTarget = true;
                        CurrentTarget = target; 
                        return;
                    }
                    else
                    {
                        _targets.Remove(target);
                    }
                }

                HaveTarget = false;
            }
            catch (Exception e)
            {
                
            }
           
        }

        private void Update()
        {
            UpdateTarget();
        }

        private bool IsTarget(Collider other) =>
            other.TryGetComponent(out ITurretTarget turretTarget);
    }
}