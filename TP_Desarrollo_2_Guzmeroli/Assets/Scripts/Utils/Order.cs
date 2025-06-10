using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class Order : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI orderNameText;
    [SerializeField] TextMeshProUGUI quantityText;

    [SerializeField] Image iconShow;
    [SerializeField] Sprite icon;

    [SerializeField] int id;
    [SerializeField] int quantity;

    [SerializeField] bool isEnable;

    public int ID {  get { return id; } }
    public bool IsEnable { get { return isEnable; } }

    private void Update()
    {
        quantityText.text = quantity.ToString();
    }

    public void SetOrder(ItemSO itemOrder, int quantity)
    {
        id = itemOrder.id;
        this.quantity = quantity;

        orderNameText.text = itemOrder.itemName;

        icon = itemOrder.icon;
        iconShow.sprite = icon;

        Enable();
    }

    public bool CheckOrder(Item item)
    {
        if (!item)
            return false;

        if(item.ID == id)
        {
            quantity -= item.SubstractItems(quantity);

            if (quantity <= 0)
            {
                Disable();
                return true;
            }
        }

        return false;
    }

    public void Enable()
    {
        isEnable = true;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        isEnable = false;
        gameObject.SetActive(false);
    }
}
