using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;


namespace DefaultNamespace
{
    public class EventSystemBN : MonoBehaviour
    {
        private ActionMaps _actionMaps;
        private Camera _camera;
        private bool _pointerDown;

        private void Awake()
        {
            _camera = Camera.main;
        }

        [Inject]
        private void Construct(ActionMaps actionMaps)
        {
            _actionMaps = actionMaps;
        }

        private void OnEnable()
        {
            _actionMaps.Main.PointerClick.performed += OnPointerPerformed;
        }

        private void OnPointerPerformed(InputAction.CallbackContext callbackContext)
        {
            _pointerDown = callbackContext.ReadValueAsButton();
            TryCastHandlers();
            
            // if (_pointerDown)
            //     PointerDown();
            // else
            //     PointerUp();
        }

        // private void PointerDown()
        // {
        //     Debug.Log("OnPointerDown");
        // }
        //
        // private void PointerUp()
        // {
        //     Debug.Log("OnPointerUp");
        // }

        private void TryCastHandlers()
        {
            Vector2 pointerPosition = _actionMaps.Main.PointerPosition.ReadValue<Vector2>();

            Ray ray = _camera.ScreenPointToRay(pointerPosition);
            
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 3f);

            RaycastHit[] hits = Physics.RaycastAll(ray, 100);

            foreach (var hit in hits)
            {
                var castedObject = hit.transform.gameObject;
          
                if (_pointerDown && castedObject.TryGetComponent(out IBNPointerDownHandler pointerDownHandler))
                    pointerDownHandler.OnPointerDown();
                
                if (_pointerDown == false && castedObject.TryGetComponent(out IBNPointerUpHandler pointerUpHandler))
                    pointerUpHandler.OnPointerUp();

            }
        }
    }
}