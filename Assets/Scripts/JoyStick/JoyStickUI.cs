using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.JoyStick
{
    public class JoyStickUI : MonoBehaviour, IDragHandler
    {
        public event Action OnMoved;

        [SerializeField] private Transform _target;
        [SerializeField] private Transform _center;
        [SerializeField] private Camera _camera;

        public Vector2 GetDirection()
        {
            return (_center.position - _target.position).normalized;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _target.position = _camera.ScreenToWorldPoint(eventData.position);

            OnMoved?.Invoke();
        }
    }
}