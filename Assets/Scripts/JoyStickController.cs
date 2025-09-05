using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static Vector2 moveDirection;
    public Vector2 worldPosition;
    public Vector2 mousePosition;
    public float mouseDragMagnitude;
    public float clampedMagnitude;
    public float maxDistancePoint;
    public bool isDragging = false;

    void Start()
    {
        worldPosition = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        if (!isDragging)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 10f);

            if (transform.localPosition.magnitude < 20f)
            {
                transform.localPosition = Vector3.zero;
                isDragging = true;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //print("dragging");
        mousePosition = eventData.position - worldPosition;
        mouseDragMagnitude = mousePosition.magnitude;
        clampedMagnitude = Mathf.InverseLerp(0f, maxDistancePoint, mouseDragMagnitude);
        transform.localPosition = mousePosition.normalized * clampedMagnitude * maxDistancePoint;
        JoyStickvalue();
    }

    public void JoyStickvalue()
    {
        float x = transform.localPosition.x;
        float y = transform.localPosition.y;

        if (x > 0)
        {
            moveDirection.x = Mathf.InverseLerp(0, maxDistancePoint, x + 1f);
        }
        if (y > 0)
        {
            moveDirection.y = Mathf.InverseLerp(0, maxDistancePoint, y + 1f);
        }
        if (x < 0)
        {
            moveDirection.x = -Mathf.InverseLerp(0, -maxDistancePoint, x - 1f);
        }
        if (y < 0)
        {
            moveDirection.y = -Mathf.InverseLerp(0, -maxDistancePoint, y - 1f);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //print("dragging stopped");
        isDragging = false;
        moveDirection = Vector2.zero;
    }
    public void ResetJoystickPosition()
    {
        isDragging = false;
        transform.localPosition = Vector2.zero;
    }
}
