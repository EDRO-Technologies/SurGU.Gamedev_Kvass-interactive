using UnityEngine;
using Zenject;

namespace Combat.Turrets
{
    public class TurretFactory : MonoBehaviour
    {
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer) => _diContainer = diContainer;

        public TurretDragDrop Get(TurretConfig turretConfig)
        {
            TurretDragDrop turretDragDrop =
                _diContainer.InstantiatePrefab(turretConfig.TurretPrefab).GetComponentInChildren<TurretDragDrop>();

            return turretDragDrop;
        }
    }
}