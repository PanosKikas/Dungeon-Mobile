using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory 
{
    event Action<ItemSlot> OnItemAdded;
    event Action<ItemSlot> OnItemRemoved;
    bool TryStore(IStorable item);
    IEnumerable<ItemSlot> Slots { get; }
    void RemoveItem(IStorable storable);
}
