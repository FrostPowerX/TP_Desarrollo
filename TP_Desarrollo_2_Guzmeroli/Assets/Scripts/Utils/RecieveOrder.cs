using UnityEngine;
using System.Collections.Generic;

public class RecieveOrder : MonoBehaviour, IInteractable
{
    public delegate void EntryFood(Inventory inventory);
    public event EntryFood OnEntryFood;

    [SerializeField] bool interacteable;

    public bool IsInteracteable()
    {
        return interacteable;
    }

    public void OnInteract(GameObject owner)
    {
        Inventory inventory = owner.GetComponent<Inventory>();

        if (inventory)
            OnEntryFood?.Invoke(inventory);
    }
}
