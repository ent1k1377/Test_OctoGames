using CodeBase.DialogueSystem;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private DialogueUI _dialogueUI;
        [SerializeField] private Transform _raycastPoint;
        
        public IInteractable Interactable { get; set; }
        public DialogueUI DialogueUI => _dialogueUI;

        [Inject] private Camera _camera;
        private Hint _hint;
        
        private void Update()
        {
            StartDialogue();
        }

        private void StartDialogue()
        {
            if (HasInteractable())
            {
                if (Input.GetKeyDown(KeyCode.E))
                    Interactable?.Interact(this);
            }
        }
        
        private bool HasInteractable()
        {
            _hint?.Hide();
            Interactable = null;
            var direction = 
                new Vector3(_camera.transform.forward.x, _raycastPoint.forward.y, _camera.transform.forward.z);
            
            var ray = new Ray(_camera.transform.position, direction);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

            if (Physics.Raycast(ray, out var hit2, Mathf.Infinity) && hit2.transform.TryGetComponent(out Hint hint))
            {
                _hint = hint;
                _hint.Show();
            }

            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity) || !hit.transform.TryGetComponent(out IInteractable interactable)) 
                return false;
            
            Interactable = interactable;
            return true;
        }
    }
}