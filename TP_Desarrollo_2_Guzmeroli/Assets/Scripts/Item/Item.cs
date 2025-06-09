using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO reference;

    [SerializeField] protected int id;
    [SerializeField] protected string itemName;
    [SerializeField] protected string description;

    [SerializeField] protected int count;

    [SerializeField] protected bool interacteable;
    [SerializeField] protected bool inInventory;

    [SerializeField] Sprite preview;
    [SerializeField] GameObject prefab;

    public string ItemName { get { return itemName; } }
    public int ID { get { return id; } }
    public int Count { get { return count; } }

    public bool InInventory { get { return inInventory; } set { inInventory = value; } }
    public Sprite Icon { get { return preview; } }
    public GameObject Prefab { get { return prefab; } }


    private void Start()
    {
        LoadReference();
        GenerateSprite();
    }

    void LoadReference()
    {
        id = reference.id;
        itemName = reference.itemName;
        description = reference.description;
    }

    [ContextMenu("Generate Sprite")]
    void GenerateSprite()
    {
#if DEBUG
        Texture2D texture = AssetPreview.GetAssetPreview(prefab);
        preview = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
#endif
    }


    public void AddCount(int count)
    {
        this.count += count;
    }

    public int SubstractItems(int count)
    {
        if (this.count >= count)
        {
            this.count -= count;
            return count;
        }
        else
        {
            int aux = this.count;
            this.count = 0;
            return aux;
        }
    }

    public bool IsInteracteable()
    {
        return interacteable;
    }

    public virtual void OnInteract(GameObject owner)
    {
        Inventory inventory = owner.GetComponent<Inventory>();

        if (inventory)
        {
            inventory.AddItem(this);
        }

        if (!inInventory)
            Destroy(gameObject);

        gameObject.SetActive(false);
    }
}
