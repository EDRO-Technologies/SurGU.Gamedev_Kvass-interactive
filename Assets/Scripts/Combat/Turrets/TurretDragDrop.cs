using Logic;
using Logic.Slots;
using Logic.Turrets;
using UnityEngine;

namespace Combat.Turrets
{
    [RequireComponent(typeof(DragDrop))]
    public class TurretDragDrop : MonoBehaviour
    {
        private LayerMask _slotLayerMask;
        private DragDrop _dragDrop;

        public Slot Slot { get; private set; } //TODO: Remove Cross dep 
        [field:SerializeField] public Turret Turret { get; private set; }

        private void Awake()
        {
            _dragDrop = GetComponent<DragDrop>();
            _slotLayerMask = LayerMask.GetMask("Slots");
        }

        private void OnEnable() => _dragDrop.DragEnded += OnEndDrag;

        private void OnDisable() => _dragDrop.DragEnded -= OnEndDrag;

        private void OnEndDrag()
        {
            Debug.DrawRay(transform.position, Vector3.down * 5, Color.yellow, 3);
            
            RaycastHit[] hits = 
                Physics.RaycastAll(transform.position, Vector3.down, 5f, _slotLayerMask);

            foreach (var hit in hits)
            {
                if (hit.transform.TryGetComponent(out IDropRaycastHandler<TurretDragDrop> dropRaycastHandler)
                    && Slot != (Slot) dropRaycastHandler)
                {
                    dropRaycastHandler.OnDropRaycast(this);
                    return;
                }
            }
            
            BackToSlot();
        }

        public void Destroy() => Destroy(gameObject);
        public void BackToSlot() => SetParent(Slot);

        public void SetSlot(Slot slot)
        {
            if (Slot != null)
                Slot.RemoveTurret();

            Slot = slot;
            SetParent(slot);
        }

        private void SetParent(Slot slot)
        {
            Turret.transform.parent = slot.TurretParent;
            Turret.transform.localPosition = Vector3.zero;
        }
    }
}