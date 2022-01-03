using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[Serializable]
public class StickPositionChangedEvent : UnityEvent<Vector2> { }

class Stick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Coroutine _coroutine;
    private Vector2 _stickCenter;
    private Vector2 _fieldCenter;
    private float _radius;
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
            _field.transform.position = Input.mousePosition;
            _stickCenter = _stick.transform.position;
            _coroutine = StartCoroutine(move());
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

    private IEnumerator move()
    {
        while (true)
        {   
            if (Vector2.Distance(_stickCenter, Input.mousePosition) <= _radius)
            {
                _stick.transform.position = Input.mousePosition;
            }
            else
            {
                _stick.transform.position = _stickCenter + ((Vector2)Input.mousePosition - _stickCenter).normalized * _radius;
            }
            OnEvent.Invoke(((Vector2)_stick.transform.position - _stickCenter) / _radius);
            yield return null;
        }
    }
}