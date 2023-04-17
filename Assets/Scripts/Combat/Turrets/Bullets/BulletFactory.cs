using UnityEngine;

namespace Combat.Turrets.Bullets
{
    public class BulletFactory : MonoBehaviour
    {
        [SerializeField] private float _bulletForce;
        [SerializeField] private int _bulletDamage;
        
        
        public void CreateBullet(Bullet bulletPrefab, Vector3 direction)
        {
            GameObject bulletObject = Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.identity);
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.Construct(_bulletDamage, direction, _bulletForce);
        }
    }
}