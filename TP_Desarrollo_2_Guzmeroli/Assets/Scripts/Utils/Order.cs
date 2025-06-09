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


    [ContextMenu("Generate Sprite")]
    void GenerateSprite(GameObject prefab)
    {
#if DEBUG
        Texture2D texture = AssetPreview.GetAssetPreview(prefab);

        if (texture)
        {
            icon = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
            iconShow.sprite = icon;
        }
#endif
    }


    public void SetOrder(ItemSO itemOrder, int quantity)
    {
        id = itemOrder.id;
        this.quantity = quantity;

        orderNameText.text = itemOrder.itemName;

        GenerateSprite(itemOrder.prefab);

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
