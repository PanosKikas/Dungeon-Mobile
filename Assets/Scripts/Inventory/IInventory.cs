using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface IInventory 
{
    public IReactiveCollection<ItemSlot> InventoryItems { get; }
    bool TryStore(IStorable item);
    IEnumerable<ItemSlot> Slots { get; }
    void RemoveItem(IStorable storable);
}
