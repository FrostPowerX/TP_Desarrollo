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
        reciever.OnEntryFood += Checkin;
        OnAllDone += OpenVictoryMenu;
        cooldown = timePerOrder;
    }

    private void OnDestroy()
    {
        reciever.OnEntryFood -= Checkin;
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

    void OpenVictoryMenu()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        victoryMenu.SetActive(true);
    }

    void Checkin(Inventory inventory)
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
}
