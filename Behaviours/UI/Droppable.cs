using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droppable : MonoBehaviour, Hoverable
{
    public int x, y;
    public ContainerType type;
    Item item;

    void Start ()
    {
        item = new Item();
        item.itemID = 0;
        item.height = 2;
        item.width = 1;
        item.shape = new bool[1, 2];
        item.shape[0, 0] = true;
        item.shape[0, 1] = true;
    }

    public void OnHover(int layer)
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (Draggable.beingDragged != null)
            {
                if (Inventory.Instance.AddItem(item, type, x - Draggable.beingDragged.x, y - Draggable.beingDragged.y))
                {
                    Draggable.beingDragged.anchorPosition = this.transform.position;
                    Draggable.beingDragged.transform.position = this.transform.position;
                    Draggable.beingDragged.transform.position = this.transform.position;
                }
            }
        }
    }
}
