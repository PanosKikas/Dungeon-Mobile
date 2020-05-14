using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class InvPickupSOEvent : UnityEvent<InventoryPickupSO> { }

public class InentoryPickup : Pickup
{
    public InventoryPickupSO item;
    
    public InvPickupSOEvent OnItemPickup;


    
    protected override void PickUp()
    {
        base.PickUp();
        Inventory.StoreToInventory(item);
    }


}


