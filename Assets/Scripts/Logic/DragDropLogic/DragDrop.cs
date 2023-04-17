using System;
using DefaultNamespace;
using UnityEngine;
using Zenject;

namespace Logic.Turrets
{
    public class DragDrop : MonoBehaviour, IBNPointerDownHandler,  IBNPointerUpHandler
    {
        [SerializeField] private LayerMask _floorLayerMask;
        private const float AboveSlots = 0.5f;
        private ActionMaps _actionMaps;
        private DragParent _dragParent;
        private Camera _camera;
        private bool _drag;

        public event Action DragBegun;
        public event Action DragEnded;

        [Inject]
        public void Construct(ActionMaps actionMaps, DragParent dragParent)
        {
            _actionMaps = actionMaps;
            _dragParent = dragParent;
        }

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if(_drag) 
                MoveDragObject();
        }

        public void OnPointerDown()
        {
            if (_drag == false)
            {
                _drag = true;
                transform.parent = _dragParent.transform;
                DragBegun?.Invoke();
            }
        }

        public void OnPointerUp()
        {
            Debug.Log("DD: PointerUp " + gameObject.name);
            if (_drag)
            {
                _drag = false;
                DragEnded?.Invoke();
            }
        }

        private void MoveDragObject()
        {
            Vector2 pointerPosition = _actionMaps.Main.PointerPosition.ReadValue<Vector2>();

            Ray ray = _camera.ScreenPointToRay(pointerPosition);
            
            if (Physics.Raycast(ray, out var hit, 100, _floorLayerMask))
            {
                Vector3 newPosition = hit.point;
                transform.position = new Vector3(newPosition.x, newPosition.y + AboveSlots, newPosition.z);
            }
        }
    }
}