using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[Serializable]
public class SliderPositionChangedEvent : UnityEvent<float> { }

public class Slider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Coroutine _coroutine;
    private Vector2 _handleCenter;
    private Vector2 _fieldCenter;
    private float _slideAreaHeight;
    [SerializeField] private RectTransform _field;
    [SerializeField] private RectTransform _handle;

    public SliderPositionChangedEvent OnEvent;

    private void Awake()
    {
        _handle.pivot = new Vector2(0.5f, 0.5f);
        _handle.transform.position = new Vector2(_handle.transform.position.x, _handle.transform.position.y + _handle.rect.height / 2);

        float slideAreaMerge = _handle.rect.height / 2;
        _field.pivot = new Vector2(_field.pivot.x, slideAreaMerge / _field.rect.height);
        _slideAreaHeight = _field.rect.height - _handle.rect.height;
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
            _handleCenter = _handle.transform.position;
            _coroutine = StartCoroutine(move());
        }
    }

    public void StopMoving()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _handle.transform.position = _handleCenter;
            _field.transform.position = _fieldCenter;
            OnEvent.Invoke(0);
            _coroutine = null;
        }
    }

    private IEnumerator move()
    {
        while (true)
        {
            if (Input.mousePosition.y >= _handleCenter.y && Input.mousePosition.y <= _handleCenter.y + _slideAreaHeight)
            {
                _handle.transform.position = new Vector2 (_handle.transform.position.x, Input.mousePosition.y);
            }
            else if (Input.mousePosition.y < _handleCenter.y)
            {
                _handle.transform.position = new Vector2(_handle.transform.position.x, _handleCenter.y);
            }
            else if (Input.mousePosition.y > _handleCenter.y + _slideAreaHeight)
            {
                _handle.transform.position = new Vector2(_handle.transform.position.x, _handleCenter.y + _slideAreaHeight);
            }
            OnEvent.Invoke((_handle.transform.position.y - _handleCenter.y) / _slideAreaHeight);
            yield return null;
        }
    }
}
