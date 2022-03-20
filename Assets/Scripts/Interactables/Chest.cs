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
            ItemSpawner.SpawnItemAtTransform(item, transform); 
        }

       
    }



    

}
