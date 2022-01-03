using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Movement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _movement; 
    float _angle;
     [SerializeField] private float _spead = 1;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float speed)
    {
        _movement.y = speed * Mathf.Sin(_angle * Mathf.PI / 180 + Mathf.PI / 2);
        _movement.x = speed * Mathf.Cos(_angle * Mathf.PI / 180 + Mathf.PI / 2);
        Debug.Log($"speed = {speed}; x = {_movement.x}; y = {_movement.y};"); 
        _rigidbody.MovePosition(_rigidbody.position + _movement * _spead * Time.fixedDeltaTime);
    }

    public void Rotate(Vector2 axes)
    {
        _angle = Vector2.Angle(Vector2.up, axes.normalized);
        if (axes.x > 0)
        {
            _angle = 180 + 180 - _angle;
        }
        if (axes.x != 0 || axes.y != 0)
        {
            _rigidbody.SetRotation(_angle);
        }
    }
}
