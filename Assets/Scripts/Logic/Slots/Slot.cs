using System;
using Combat.Turrets;
using UnityEngine;
using Zenject;

namespace Logic.Slots
{
    public class Slot : MonoBehaviour, IDropRaycastHandler<TurretDragDrop>
    {
        public TurretDragDrop TurretDragDrop { get; private set; }
        private TurretFactory _turretFactory;

        [field: SerializeField] public Transform TurretParent { get; private set; }

        public bool Empty => TurretDragDrop == null;

        public event Action ItemMerged;

        [Inject]
        private void Construct(TurretFactory turretFactory) => _turretFactory = turretFactory;

        public void OnDropRaycast(TurretDragDrop turretDragDrop) => TrySetTurret(turretDragDrop);

        public void TrySetTurret(TurretDragDrop turretDragDrop)
        {
            if (Empty)
                SetTurret(turretDragDrop);
            else if (CanBeMerged(turretDragDrop))
                Merge(turretDragDrop);
            else
            {
                Debug.Log("Slot: Back");
                turretDragDrop.BackToSlot();
            }
                
            
            //TODO: Fix Swap(turret);
        }

        public void RemoveTurret()
        {
            TurretDragDrop = null;
        }

        private void SetTurret(TurretDragDrop turretDragDrop)
        {
            TurretDragDrop = turretDragDrop;
            TurretDragDrop.SetSlot(this);
        }

        private void Merge(TurretDragDrop turretDragDrop)
        {
            ItemMerged?.Invoke();
            TurretDragDrop mergedTurretDragDrop = _turretFactory.Get(turretDragDrop.Turret.MergedTurretConfig);
            turretDragDrop.Destroy();
            TurretDragDrop.Destroy();

            SetTurret(mergedTurretDragDrop);
        }

        private bool CanBeMerged(TurretDragDrop turretDragDrop) =>
            turretDragDrop.Turret.TurretConfig == TurretDragDrop.Turret.TurretConfig &&
            turretDragDrop.Turret.MergedTurretConfig != null;

        private void Swap(TurretDragDrop turretDragDrop)
        {
            var tempTurret = TurretDragDrop;

            (turretDragDrop.Slot.TurretDragDrop, TurretDragDrop) = (TurretDragDrop, turretDragDrop.Slot.TurretDragDrop);

            turretDragDrop.SetSlot(turretDragDrop.Slot);
            // Debug.Log(turret.);
            TurretDragDrop.SetSlot(this);

            //outTurret/turret  = insideTurret/Turret 
            // Debug.Log($"outTurret.Slot {outTurret.Slot.name} outTurret.Slot.Turret.name {outTurret.Slot.Turret.name}");

            //outTurret/turret/insideTurret/Turret = outTurret/turret/insideTurret/Turret
            // Debug.Log($"outTurret {outTurret}");
        }
    }
}