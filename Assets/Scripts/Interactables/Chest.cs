using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chest : Interactable
{

    [Header("Open/Closed Chest"), SerializeField]
    GameObject openChest;
    [SerializeField]
    GameObject closedChest;

    [SerializeField] private Pickup pickupPrefab;
    
    [SerializeField]
    public PickupSO[] loot;

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
        foreach (var item in loot)
        {
            SpawnPickup(item); 
        }
    }
    
    private void SpawnPickup(PickupSO item)
    {
        var pickup = Instantiate(pickupPrefab, transform.position + new Vector3(0, -.3f, 0), Quaternion.identity);
        pickup.SetTo(item);
    }
}
