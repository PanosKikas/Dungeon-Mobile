using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory 
{
    bool TryStore(IStorable item);
    IStorable GetItemOnSlot(int index);
    IEnumerable<ItemSlot> Slots { get; }
}
