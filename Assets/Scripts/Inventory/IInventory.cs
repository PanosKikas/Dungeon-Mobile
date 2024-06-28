using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UniRx;
using UnityEngine;

public interface IInventory
{
    public IReactiveCollection<ItemSlot> InventoryItems { get; }
    bool IsFull();
    void Store(IStorable item);
    IEnumerable<ItemSlot> Slots { get; }
    void RemoveItem(IStorable storable);
    bool ContainsItem(IStorable storable);
}
