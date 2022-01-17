using GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameUI
{
    public enum JoystickState
    {
        Calm,
        Pressed
    }

    public interface IJoystick
    {
        void Prepare();
        JoystickState GetState();
        Vector3 GetDirection();
    }

    public class Joystick : MonoBehaviour, IJoystick, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
    {
        [SerializeField] private Image _backCircle;
        [SerializeField] private Image _tip;

        private float _backCircleRadius;
        private Vector3 _startTipPosition;
        private Vector3 _direction;

        private IHitController _hitController;

        private JoystickState _joystickState;

        private void OnDestroy()
        {
            Helper.ServiceLocator.SharedInstanse.Unregister<IJoystick>();
        }

        private void Start()
        {
            Prepare();
        }

        public void Prepare()
        {
            Helper.ServiceLocator.SharedInstanse.Register<IJoystick>(this);
            _hitController = Helper.ServiceLocator.SharedInstanse.Resolve<IHitController>();

            CalculateBackCircleRadius();
            SetState(JoystickState.Calm);

            transform.SetSiblingIndex(0);
        }

        public Vector3 GetDirection()
        {
            return _direction;
        }

        public JoystickState GetState()
        {
            return _joystickState;
        }

#region INPUT EVENTS

        public void OnPointerDown(PointerEventData eventData)
        {
            _backCircle.transform.position = eventData.position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            SetState(JoystickState.Pressed);
            _startTipPosition = _tip.transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 newTipPosition;
            float distance;
            distance = Vector3.Distance(_startTipPosition, eventData.position);
            _direction = new Vector3(eventData.position.x, eventData.position.y) - _startTipPosition;
            _direction = _direction.normalized;

            if (distance > _backCircleRadius)
            {
                newTipPosition = _startTipPosition + _direction * _backCircleRadius;
                distance = _backCircleRadius;
            }
            else
            {
                _direction *= distance / _backCircleRadius;
                newTipPosition = eventData.position;
            }

            _hitController.SetHitOptions(_direction, distance);

            _tip.transform.position = newTipPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _tip.transform.position = _startTipPosition;

            _hitController.Hit();

            //event About HIT
            //OnPointerRelease?.Invoke();
            SetState(JoystickState.Calm);
        }

#endregion 

        private void SetState(JoystickState state)
        {
            _joystickState = state;
        }

        private void CalculateBackCircleRadius()
        {
            if (_backCircle.TryGetComponent<RectTransform>(out var rectTransform))
            {
                _backCircleRadius = rectTransform.sizeDelta.x * Helper.GameСonstants.Half;
            }
        }
    }
}



