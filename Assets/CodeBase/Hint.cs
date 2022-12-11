using CodeBase.Extensions;
using UnityEngine;
using Zenject;

namespace CodeBase
{
    public class Hint : MonoBehaviour
    {
        [SerializeField] private Transform _hintPoint;
        [SerializeField] private RectTransform _hintUI;

        [Inject] private Camera _camera;
        
        private void Update()
        {
            _hintUI.position = _camera.WorldToScreenPoint(_hintPoint.position);
        }

        public void Show() => 
            _hintUI.gameObject.Activate();
        
        public void Hide() => 
            _hintUI.gameObject.Deactivate();
    }
}