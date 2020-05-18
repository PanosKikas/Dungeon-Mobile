using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(ItemSpawner))]
public class Chest : Interactable
{

    [Header("Open/Closed Chest"), SerializeField]
    GameObject openChest;
    [SerializeField]
    GameObject closedChest;

    [Header("Loot"), SerializeField]
    PotionSO[] loot;

    ItemSpawner itemSpawner;

    private void Awake()
    {
        itemSpawner = GetComponent<ItemSpawner>();
    }

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
            itemSpawner.SpawnItem(item);   
        }
    }



    

}
