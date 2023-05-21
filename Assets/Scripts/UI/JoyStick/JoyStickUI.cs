using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.JoyStick
{
    public class JoyStickUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public event Action OnMoveStarted;
        public event Action OnMoveEnded;

        [SerializeField] private RectTransform _target;
        [SerializeField] private RectTransform _center;
        [SerializeField] private float _clampLenght;

        public Vector2 GetDirection()
        {
            return (_target.position - _center.position).normalized;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnMoveStarted?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _target.position = (Vector2) eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _target.position = _center.position;

            OnMoveEnded?.Invoke();
        }
    }
}