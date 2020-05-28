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
        bool stored = Inventory.Instance.TryStoreInventory(InventoryPickupStats);
        if (!stored)
            return;
        base.PickUp();

    }


}


