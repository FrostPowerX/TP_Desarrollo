using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> items;

    public void AddItem(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
            { 
                items[i].AddCount(item.Count);
                return;
            }
        }

        items.Add(item);
        item.InInventory = true;
    }

    public Item FindItemByID(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == id)
                return items[i];
        }

        return null;
    }

    public Item FindItemByName(string name)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ItemName == name)
                return items[i];
        }

        return null;
    }
}
