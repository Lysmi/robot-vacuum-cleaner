using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Movement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    [SerializeField] private float _spead = 1;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float x, float y)
    {
        _movement.x = x;
        _movement.y = y;
    }

    private void FixedUpdate()
    {
        float angle = Vector2.Angle(Vector2.up, _movement.normalized);
        if (_movement.x > 0)
        {
            angle *= -1;
        }
        if (_movement.x != 0 || _movement.y != 0)
        {
            _rigidbody.SetRotation(angle);
        }
        _rigidbody.MovePosition(_rigidbody.position + _movement * _spead * Time.fixedDeltaTime);
    }
}
