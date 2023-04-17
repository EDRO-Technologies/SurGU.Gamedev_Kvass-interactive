using Combat.Turrets;
using UnityEngine;
using Zenject;

namespace Logic.Slots
{
    public class SlotGrid : MonoBehaviour
    {
        [ContextMenuItem("Add turret", "DEBUG_METHOD_ADD3TURRET")] [SerializeField]
        public Slot[] Slots;

        [SerializeField] public TurretConfig TESTTURRET;
        [SerializeField] public TurretConfig TESTMORTAR;
        private TurretFactory _turretFactory;

        [Inject]
        private void Construct(TurretFactory turretFactory) => _turretFactory = turretFactory;


        private void Start() //TODO: DELETE POST TEST
        {
            DEBUG_METHOD_ADD3TURRET();
        }


        private void DEBUG_METHOD_ADD3TURRET()
        {
            AddTurret(_turretFactory.Get(TESTTURRET));
            AddTurret(_turretFactory.Get(TESTTURRET));
            AddTurret(_turretFactory.Get(TESTMORTAR));
        }

        public void AddTurret(TurretDragDrop turretDragDrop)
        {
            foreach (var slot in Slots)
            {
                if (slot.Empty)
                {
                    slot.TrySetTurret(turretDragDrop);
                    return;
                }
            }

            turretDragDrop.Destroy();
        }

        public int GetTurretsLevelSum()
        {
            int sum = 0;

            foreach (var slot in Slots)
            {
                if (slot.Empty == false)
                {
                    sum += slot.TurretDragDrop.Turret.Level;
                }
            }

            return sum;
        }
    }
}