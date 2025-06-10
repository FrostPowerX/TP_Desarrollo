using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] ItemSO item;

    [SerializeField] int slot;
    [SerializeField] int count;
    [SerializeField] int maxCount;

    [SerializeField] bool empty;

    public ItemSO Item { get { return item; } }

    public void Add(ItemSO item, int count)
    {
        if (count <= 0)
            return;

        empty = false;
    }

    public void Remove(ItemSO item)
    {

    }
}
