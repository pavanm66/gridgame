using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] bool pressed;
    [SerializeField] private OnChange onPressed;
    [SerializeField] private OnChange onclicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        onclicked.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }

    void Update()
    {
        if (pressed)
        {
            onPressed.Invoke();
        }
    }


}
