using UnityEditor;
using UnityEngine;
using static Stove;

public class Stove : MonoBehaviour, IInteractable
{
    public delegate void IconGenerated();
    public event IconGenerated OnIconGenerated;

    public delegate void CoockingStart();
    public event CoockingStart OnCoockingStart;

    public delegate void CoockingDone();
    public event CoockingDone OnCoockingDone;

    public delegate void CoockingEmpty();
    public event CoockingEmpty OnCoockingEmpty;

    [SerializeField] int acceptID;

    [SerializeField] int ingredientNeeded;
    [SerializeField] int itemsOnStove;

    [SerializeField] float coockingTime;

    [SerializeField] bool interacteable;

    [SerializeField] GameObject objView;
    [SerializeField] GameObject coockedItem;
    [SerializeField] int coockedItemCount;

    [SerializeField] float timeSpend;

    Sprite iconResult;

    bool coocking;
    bool coockingDone;

    public float TimeSpend { get { return timeSpend; } }
    public Sprite IconResult { get { return iconResult; } }

    void Start()
    {
        coockingDone = false;
        coocking = false;
        timeSpend = 0;
        objView.SetActive(false);
    }

    void Update()
    {
        if (!iconResult)
            GenerateSprite();

        timeSpend -= (timeSpend > 0) ? Time.deltaTime : timeSpend;

        if (coocking)
            OnCoocking();
    }

    [ContextMenu("Generate Sprite")]
    void GenerateSprite()
    {
        Texture2D texture = AssetPreview.GetAssetPreview(coockedItem);

        if (texture)
        {
            iconResult = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
            OnIconGenerated?.Invoke();
        }
    }

    void StartCoocking()
    {
        coocking = true;
        timeSpend = coockingTime;
        itemsOnStove = 0;

        objView.SetActive(true);
        OnCoockingStart?.Invoke();
    }

    void OnCoocking()
    {
        if (timeSpend <= 0)
        {
            coocking = false;
            coockingDone = true;
            OnCoockingDone?.Invoke();
        }
    }

    void ServeFood()
    {
        GameObject newItem = Instantiate(coockedItem, objView.transform.position, objView.transform.rotation);
        newItem.GetComponent<Item>().AddCount(coockedItemCount);

        OnCoockingEmpty?.Invoke();
        objView.SetActive(false);
    }

    public bool IsInteracteable()
    {
        return interacteable;
    }

    public void OnInteract(GameObject owner)
    {
        if (coocking)
            return;

        if (coockingDone)
        {
            ServeFood();
            coockingDone = false;
        }
        else
        {
            Inventory inventory = owner.GetComponent<Inventory>();

            if (inventory)
            {
                Item item = inventory.FindItemByID(acceptID);

                if (item)
                {
                    itemsOnStove += item.SubstractItems(ingredientNeeded - itemsOnStove);

                    if (itemsOnStove == ingredientNeeded)
                        StartCoocking();
                }
            }
        }
    }
}
