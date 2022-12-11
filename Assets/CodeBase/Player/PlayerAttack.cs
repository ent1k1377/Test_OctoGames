using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private readonly int _attack = Animator.StringToHash("Attack");
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                _animator.SetTrigger(_attack);
        }
    }
}