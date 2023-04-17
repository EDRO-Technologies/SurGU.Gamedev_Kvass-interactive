using Combat.Turrets.Bullets;
using UnityEngine;

namespace Combat.Turrets
{
    [CreateAssetMenu(menuName = "TurretConfig", fileName = "TurretConfig", order = 0)]
    public class TurretConfig : ScriptableObject
    {
        public readonly float MaxTurnSpeed = 5f;
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public TurretConfig MergedTurretConfig { get; private set; }
        [field: SerializeField] public Turret TurretPrefab { get; private set; }
        
        [field: Header("Shoot")]
        [field: SerializeField] public float ReloadTime{ get; private set; }
        [field: SerializeField] public Bullet BulletPrefab{ get; private set; }
    }
}