using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Stick _stick;
    [SerializeField] private Movement _movement;

    private void Awake()
    {
        _stick.SetOutputFunk((x, y) => _movement.Move(x, y));
    }

}
