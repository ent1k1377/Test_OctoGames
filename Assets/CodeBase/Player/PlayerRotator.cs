using CodeBase.GeneralSystems;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    public class PlayerRotator : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _playerObj;
        [SerializeField] private Transform _oriented;
        [SerializeField] private float _rotationSpeed;

        [Inject] private CursorSystem _cursorSystem;
        
        private void Start() => 
            _cursorSystem.ActivateGameMode();

        private void Update()
        {
            var direction = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
            _oriented.forward = direction.normalized;
            
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            var inputDirection = _oriented.forward * verticalInput + _oriented.right * horizontalInput;
            
            if (inputDirection != Vector3.zero)
                _playerObj.forward = Vector3.Slerp(_playerObj.forward, inputDirection.normalized, _rotationSpeed * Time.deltaTime);
        }
    }
}
