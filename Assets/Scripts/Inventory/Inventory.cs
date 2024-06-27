using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class ItemSlot
{
    public IStorable Item { get; private set; }
    public ReactiveProperty<int> Stack { get; } = new(0);

    public void Store(IStorable itemToStore)
    {
        if (Item == null)
        {
            Item = itemToStore;
            Stack.Value = 1;
            return;
        }

        if (Item != null && !itemToStore.Equals(Item))
        {
            throw new InvalidOperationException("Cannot store different items to the same inventory slot");
        }

        Item = itemToStore;
        ++Stack.Value;
    }
    
    public void RemoveItem()
    {
        --Stack.Value;
        if (Stack.Value == 0)
        {
            Clear();
        }
    }

    public bool IsEmpty()
    {
        return Item == null;
    }

    private void Clear()
    {
        Stack.Value = 0;
        Item = null;
    }
}

public class Inventory : IInventory
{
    private const int InitialCapacity = 20;
    public IEnumerable<ItemSlot> Slots => InventoryItems;
    private int currentCapacity;
    public IReactiveCollection<ItemSlot> InventoryItems { get; } = new ReactiveCollection<ItemSlot>();
    public Inventory()
    {
        currentCapacity = InitialCapacity;
    }

    bool IsInventoryFull()
    {
        return InventoryItems.Count() == currentCapacity;
    }

    public bool TryStore(IStorable itemToStore)
    {
        ItemSlot slot = InventoryItems.FirstOrDefault(s => s.Item.Equals(itemToStore));
        if (slot == null)
        {
            if (IsInventoryFull())
            {
                return false;
            }

            slot = new ItemSlot();
            slot.Store(itemToStore);
            InventoryItems.Add(slot);
        }
        else
        {
            slot.Store(itemToStore);
        }
        
        return true;
    }

    public void RemoveItem(IStorable storable)
    {
        var slot = InventoryItems.FirstOrDefault(s => s.Item.Equals(storable));
        
        if (slot == null)
        {
            Debug.LogError("No item " + storable.Name + " found in inventory of character to remove.");
            return;
        }

        slot.RemoveItem();
        if (slot.IsEmpty())
        {
            InventoryItems.Remove(slot);
        }
    }
}
