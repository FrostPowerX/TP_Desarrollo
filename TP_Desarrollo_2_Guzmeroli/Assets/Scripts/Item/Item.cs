using System;
using System.IO;
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

    [SerializeField] Sprite icon;

    public string ItemName { get { return itemName; } }
    public int ID { get { return id; } }
    public int Count { get { return count; } }

    public bool InInventory { get { return inInventory; } set { inInventory = value; } }
    public Sprite Icon
    {
        get
        {
            if (icon)
                return icon;
            else return reference.icon;
        }
    }
    public GameObject Prefab { get { return reference.prefab; } }


    private void Start()
    {
        LoadReference();
    }

    void LoadReference()
    {
        id = reference.id;
        itemName = reference.itemName;
        description = reference.description;
        icon = reference.icon;
    }

#if DEBUG
    [ContextMenu("Save Image PNG")]
    void CreatePNG()
    {
        Texture2D texture = AssetPreview.GetAssetPreview(reference.prefab);

        if (!texture)
        {
            Debug.LogWarning($"Missing Texture. {this.ToString()} : Item Name {itemName}");
            return;
        }

        byte[] bytes = texture.EncodeToPNG();

        string path = Path.Combine(Application.persistentDataPath, $"Textures/{itemName}.png");
        File.WriteAllBytes(path, bytes);

        Debug.Log("Texture save on: " + path);
    }
#endif

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
