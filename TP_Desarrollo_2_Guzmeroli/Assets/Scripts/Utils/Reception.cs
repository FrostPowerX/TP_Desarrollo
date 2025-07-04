using UnityEngine;
using System.Collections.Generic;
using static Stove;
using UnityEditor;

public class Reception : MonoBehaviour
{
    public delegate void AllDone();
    public event AllDone OnAllDone;

    [SerializeField] GameObject victoryMenu;

    [SerializeField] RecieveOrder reciever;
    [SerializeField] List<Order> ordersSlots;
    [SerializeField] List<ItemSO> itemsOrderList;

    [SerializeField] float timePerOrder;

    [SerializeField] int minQuantity;
    [SerializeField] int maxQuantity;

    [SerializeField] int remainingOrders;

    int inOrder;

    float cooldown;

    void Start()
    {
        reciever.OnEntryFood += CheckIn;
        cooldown = timePerOrder;
    }

    void OnDestroy()
    {
        reciever.OnEntryFood -= CheckIn;
    }

    void Update()
    {
        cooldown -= (cooldown > 0) ? Time.deltaTime : cooldown;

        if (cooldown <= 0 && remainingOrders > 0)
            GenerateOrders();

        if (remainingOrders <= 0 && inOrder <= 0)
        {
            OnAllDone?.Invoke();
            this.enabled = false;
        }
    }

    void CheckIn(Inventory inventory)
    {
        for (int i = 0; i < ordersSlots.Count; i++)
            if (ordersSlots[i].IsEnable)
                if (ordersSlots[i].CheckOrder(inventory.FindItemByID(ordersSlots[i].ID)))
                    inOrder--;
    }

    void GenerateOrders()
    {
        Order order = GetEmptyOrderSlot();

        if (order)
        {
            order.SetOrder(GetRandomItem(), Random.Range(minQuantity, maxQuantity + 1));
            cooldown = timePerOrder;
            remainingOrders--;
            inOrder++;
        }
    }

    Order GetEmptyOrderSlot()
    {
        for (int i = 0; i < ordersSlots.Count; i++)
        {
            if (!ordersSlots[i].IsEnable)
                return ordersSlots[i];
        }

        return null;
    }

    ItemSO GetRandomItem()
    {
        int rand = Random.Range(0, itemsOrderList.Count);

        return itemsOrderList[rand];
    }

    public void SetOrderTime(float time) => timePerOrder = time;
    public void SetMinQuantity(int quantity) => minQuantity = quantity;
    public void SetMaxQuantity(int quantity) => maxQuantity = quantity;
    public void SetMaxOrders(int value) => remainingOrders = value;
}
