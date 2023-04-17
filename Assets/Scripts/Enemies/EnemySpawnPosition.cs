using UnityEngine;

namespace Enemies
{
    public class EnemySpawnPosition : MonoBehaviour
    {
        [SerializeField] private float Range;
        private Vector3 _startPosition;

        private void Awake()
        {
            _startPosition = transform.position;
        }

        public void UpdatePosition()
        {
            var xPositionModif = Random.Range(-Range, Range);
            transform.position = new Vector3(_startPosition.x + xPositionModif, _startPosition.y, _startPosition.z);
        }
    }
}