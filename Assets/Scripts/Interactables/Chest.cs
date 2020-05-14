using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Chest : Interactable
{

    [Header("Open/Closed Chest"), SerializeField]
    GameObject openChest;
    [SerializeField]
    GameObject closedChest;

    [Header("Loot"), SerializeField]
    GameObject[] loot;


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
            Instantiate(item, transform.position + new Vector3(0, -.3f, 0), Quaternion.identity);
        }
    }



    

}
