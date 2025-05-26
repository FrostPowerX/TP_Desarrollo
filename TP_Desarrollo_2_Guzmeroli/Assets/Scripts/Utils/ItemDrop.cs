using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class ItemDrop : MonoBehaviour
{
    [SerializeField] List<GameObject> itemsDrop;

    [SerializeField] int quantity;

    HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnDeath += Drop;
    }

    void Drop()
    {
        int rand = Random.Range(0, itemsDrop.Count);

        GameObject newItem = Instantiate(itemsDrop[rand], transform.position, transform.rotation);
        newItem.GetComponent<Item>().AddCount(quantity);
    }
}
