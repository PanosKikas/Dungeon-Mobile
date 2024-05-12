using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chest : Interactable
{

    [Header("Open/Closed Chest"), SerializeField]
    GameObject openChest;
    [SerializeField]
    GameObject closedChest;

    [Header("Loot"), SerializeField]
    public ItemData[] loot;

    [SerializeField]
    public PickupObject pickupPrefab;

    private ItemFactory _itemFactory = new ItemFactory();

    public override void Interact()
    {
        base.Interact();
        OpenChest();
    }

    void OpenChest()
    {
        openChest.SetActive(true);
        SpawnLoot();
        closedChest.SetActive(false);
        this.enabled = false;
    }

    void SpawnLoot()
    {
        foreach (var itemData in loot)
        {
            var item = _itemFactory.Create(itemData);
            PickupObject prefab = Instantiate(pickupPrefab, transform.position + new Vector3(0, -.3f, 0), Quaternion.identity);
            prefab.InitializeTo(item);
        }  
    }
}
