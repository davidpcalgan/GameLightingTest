using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ContainerType { Back, LeftHip, RightHip, LeftHand, RightHand };

public sealed class Inventory
{
    private Inventory() {
        back = new Container(2, 4);
        leftHip = new Container(1, 1);
        rightHip = new Container(1, 1);
        leftHand = new Container(1, 1);
        rightHand = new Container(1, 1);
    }
    private static Inventory instance = null;
    public static Inventory Instance { get {
            if (instance == null) {
                instance = new Inventory();
            }
            return instance; 
        } 
    }

    Container back, leftHip, rightHip, leftHand, rightHand;

    public bool AddItem(Item item, ContainerType container, int x, int y)
    {
        switch (container)
        {
            case ContainerType.Back:
                return back.AddItem(item, x, y);

            case ContainerType.LeftHip:
                return leftHip.AddItem(item, x, y);

            case ContainerType.RightHip:
                return rightHip.AddItem(item, x, y);

            case ContainerType.LeftHand:
                return leftHand.AddItem(item, x, y);

            case ContainerType.RightHand:
                return rightHand.AddItem(item, x, y);
        }
        return false;
    }

    class Container
    {
        public int width, height;
        public Item[,] items;

        public Container (int _width, int _height)
        {
            height = _height;
            width = _width;
            items = new Item[width, height];
        }

        public bool RemoveItem (Item item)
        {
            bool removed = false;
            for (int i = 0; i < item.width; i++)
            {
                for (int j = 0; j < item.height; j++)
                {
                    if (items[i, j] != null && items[i, j].itemID == item.itemID)
                    {
                        items[i, j] = null;
                        removed = true;
                    }
                }
            }
            return removed;
        }

        public bool AddItem (Item item, int x, int y)
        {
            if (x + item.width <= width && y + item.height <= height)
            {
                for (int i = 0; i < item.width; i++)
                {
                    for (int j = 0; j < item.height; j++)
                    {
                        if (item.shape[i, j] && items[x + i, y + j] != null && items[x + i, y + j].itemID != item.itemID)
                        {
                            Debug.Log("blocked");
                            return false;
                        }
                    }
                }

                RemoveItem(item);

                for (int i = 0; i < item.width; i++)
                {
                    for (int j = 0; j < item.height; j++)
                    {
                        if (item.shape[i,j])
                        {
                            items[x + i, y + j] = item;
                        }
                    }
                }
                return true;
            }
            Debug.Log("OOB");
            return false;
        }
    }


}

public class Item
{
    public int itemID;
    public int height, width;
    public bool[,] shape;
}
