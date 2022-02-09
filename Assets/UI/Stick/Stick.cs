using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Hectic.Mobile.Controllers
{

    [Serializable]
    public class StickPositionChangedEvent : UnityEvent<Vector2> { }

    class Stick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Coroutine _coroutine;
        private Vector2 _stickCenter;
        private Vector2 _fieldCenter;
        private float _radius;
        private bool busy;
        [SerializeField] private RectTransform _field;
        [SerializeField] private RectTransform _stick;

        public StickPositionChangedEvent OnEvent;

        private void Awake()
        {
            _radius = _field.rect.width / 2;
            _fieldCenter = _field.position;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            StartMoving();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopMoving();
        }

        public void StartMoving()
        {
            if (_coroutine == null)
            {
                Touch touch = Input.GetTouch(0);
                if (Input.touchCount > 1)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        Touch currentTouch = Input.GetTouch(i);
                        float difference = Vector2.Distance(_stickCenter, currentTouch.position) - _radius;
                        if (difference > 0)
                        {
                            touch = currentTouch;
                            break;
                        }
                    }
                }
                _field.transform.position = touch.position;
                _stickCenter = _stick.transform.position;
                _coroutine = StartCoroutine(move(touch));
            }
        }

        public void StopMoving()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _stick.transform.position = _stickCenter;
                _field.transform.position = _fieldCenter;
                _coroutine = null;
            }
        }

        private IEnumerator move(Input touch)
        {

            while (true)
            {
                move_(touch);
                yield return null;
            }
        }

        private void move_(Touch touch)
        {
            if (Vector2.Distance(_stickCenter, touch.position) <= _radius)
            {
                _stick.transform.position = touch.position;
            }
            else
            {
                _stick.transform.position = _stickCenter + ((Vector2)touch.position - _stickCenter).normalized * _radius;
            }
            OnEvent.Invoke(((Vector2)_stick.transform.position - _stickCenter) / _radius);
        }
    }
}