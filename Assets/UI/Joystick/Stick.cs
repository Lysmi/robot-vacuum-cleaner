using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Coroutine _coroutine;
    private Vector2 _center;
    private float _radius;
    [SerializeField] private RectTransform _background;

    public delegate void Funk(float x, float y);

    private Funk _funk;

    public void SetOutputFunk(Funk funk)
    {
        _funk = funk;
    }

    private void Awake()
    {
        _radius = _background.rect.width / 2;
        _center = transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartMoving();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopMoving();
    }

    private IEnumerator move()
    {
        while (true)
        {
            

            if (Vector2.Distance(_center, Input.mousePosition) <= _radius)
            {
                transform.position = Input.mousePosition;
            }
            else
            {
                transform.position = _center + ((Vector2)Input.mousePosition - _center).normalized * _radius;
            }
            _funk((transform.position.x - _center.x) / _radius, (transform.position.y - _center.y) / _radius);
            yield return null;
        }
    }

    public void StartMoving()
    {
        _coroutine = StartCoroutine(move());
    }

    public void StopMoving()
    {
        StopCoroutine(_coroutine);

        transform.position = _center;

        _funk(0, 0);
    }
}
