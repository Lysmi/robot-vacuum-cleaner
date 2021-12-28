using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StickOutline : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Stick _stick;

    public void OnPointerDown(PointerEventData eventData)
    {
        _stick.transform.position = Input.mousePosition;
        _stick.StartMoving();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _stick.StopMoving();
    }

    public void SetOutputFunk(Stick.Funk funk)
    {
        _stick.SetOutputFunk(funk);
    }
}
