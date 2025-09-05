using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeGesture
{
    public static SwipeDirection GetSwipeDirection(float y , float x)
    {
        SwipeDirection sd = SwipeDirection.none;
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        if (angle >= 45f && angle <= 135f)
        {
            sd = SwipeDirection.up;
        }
        if (angle >= -45f && angle < 45f)
        {
            sd = SwipeDirection.right;
        }
        if (angle < -45f && angle >= -135f)
        {
            sd = SwipeDirection.down;
        }
        if (angle < -135f && angle >= -180f)
        {
            sd = SwipeDirection.left;
        }
        if (angle > 135f && angle <= 180f)
        {
            sd = SwipeDirection.left;
        }
        return sd;
    }
    public static Vector2 SwipeVector(float y, float x)
    {
        SwipeDirection swipeDirection = SwipeGesture.GetSwipeDirection(y,x);

        switch (swipeDirection)
        {
            case SwipeDirection.none:
                return Vector2.zero;
                break;
            case SwipeDirection.up:
                return Vector2.up;
                break;
            case SwipeDirection.down:
                return Vector2.down;
                break;
            case SwipeDirection.right:
                return Vector2.right;
                break;
            case SwipeDirection.left:
                return Vector2.left;
                break;
            default:
                return Vector2.zero;
                break;
        }
    }
}
public enum SwipeDirection
{
    none, up, down, right, left
}
