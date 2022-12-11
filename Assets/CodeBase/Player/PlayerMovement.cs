using System;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _oriented;
        [SerializeField] private float _speed;
        
        private readonly int _movement = Animator.StringToHash("Movement");

        private void FixedUpdate()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");

            _animator.SetFloat(_movement, Math.Abs(horizontalInput) + Math.Abs(verticalInput));

            var inputDirection = _oriented.forward * verticalInput + _oriented.right * horizontalInput;
            if (inputDirection != Vector3.zero)
                transform.Translate(inputDirection * _speed * Time.fixedDeltaTime);
        }
    }
}
