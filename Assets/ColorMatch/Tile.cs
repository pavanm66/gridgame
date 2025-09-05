 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 cellIndex;
    public int colorIndex;
    public Vector2 originalPosition;
    public string colorName;
    public SpriteRenderer spriteRenderer;
    public bool isMatched = false;
    void Awake()
    {
        originalPosition = gameObject.transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetOriginalPosition()
    {
        gameObject.transform.position =  originalPosition;
    }
    public void Matched()
    {
        isMatched = true;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
    }
    public void UnMatched()
    {
        isMatched = false;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
    }
    public void ChangeColor(Customcolor _custColor, int _colorIndex)
    {
        colorName = _custColor.colorName;
        colorIndex = _colorIndex;
        spriteRenderer.color = _custColor.color;
        isMatched = false;
    }
}
