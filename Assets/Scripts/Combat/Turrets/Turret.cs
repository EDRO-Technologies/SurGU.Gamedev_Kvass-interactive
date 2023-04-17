using UnityEngine;

namespace Combat.Turrets
{
    public class Turret : MonoBehaviour
    {
        [field: SerializeField] public TurretConfig TurretConfig { get; private set; }
        public TurretConfig MergedTurretConfig { get; private set; }
        public int Level { get; private set; }

        private void Awake()
        {
            MergedTurretConfig = TurretConfig.MergedTurretConfig;
            Level = TurretConfig.Level;
        }
    }
}