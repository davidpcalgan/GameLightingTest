using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface Hoverable
{
    public abstract void OnHover(int layer);
}

public class Draggable : MonoBehaviour, Hoverable
{
    public int x, y;

    public static Draggable beingDragged;
    public bool isDragging;
    public Vector3 anchorPosition;

    void Start()
    {
        isDragging = false;
        anchorPosition = this.transform.position;
        beingDragged = null;
    }

    public void DragWith (Vector3 location, Vector3 offset)
    {
        this.transform.position = location + offset;
    }

    public void OnHover (int layer)
    {
        if (isDragging)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                this.transform.position = anchorPosition;
                beingDragged = null;
            }
        }
        else if (Input.GetMouseButtonDown(0) && layer == 0)
        {
            isDragging = true;
            beingDragged = this;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            this.transform.position = Input.mousePosition;
        }
    }
}
