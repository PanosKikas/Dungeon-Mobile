using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public abstract class InventoryPickup<T>: Pickup<T> where T: InventoryPickupSO
{

    protected override void PickUp()
    {
        StoreToInventory(PickupStats);
        base.PickUp();

    }
    protected void StoreToInventory(InventoryPickupSO item)
    {
        Inventory.Instance.StoreToInventory(item);
    }

    
    

}


