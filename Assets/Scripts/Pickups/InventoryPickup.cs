using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class InventoryPickup : Pickup 
{ 

    InventoryPickupSO InventoryPickupStats
    {
        get
        {
            return (InventoryPickupSO)PickupStats;
        }
    }


    protected override void PickUp()
    {
        StoreToInventory(InventoryPickupStats);
        base.PickUp();

    }
    protected void StoreToInventory(InventoryPickupSO item)
    {
        Inventory.Instance.StoreToInventory(item);
    }

}


