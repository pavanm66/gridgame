using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayStore : MonoBehaviour
{
    public GameObject selectedObj;
    public GameObject removeObj;
    public GameObject[] elements;

    RaycastHit2D hit;
    Vector2 mouseLocation;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mouseLocation, Vector2.zero);
            if (hit)
            {
                selectedObj = hit.collider.gameObject;
                Adding(selectedObj);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Remove();
        }
    }
    void Adding(GameObject obj)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            if (elements[i] == null)
            {
                elements[i] = obj;
                return;
            }
        }
    }
    
    void Remove()
    {
        bool retur = false;
        for (int i = elements.Length-1; i >= 0 ; i--)
        {
            if (elements[i] != null)
            {
                removeObj = elements[i];
                elements[i] = null;
                i = -1;
                retur = true;
            }
        }
        if (!retur)
        {
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                if (i+1<elements.Length-1)
                {
                    elements[i] = elements[i + 1];
                }
                if (i == elements.Length-1)
                {
                    elements[i] = selectedObj;
                }
            }
        }
    }
}
