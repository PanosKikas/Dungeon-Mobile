using DMT.Character.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ItemSlot
{
    public IStorable Item { get; private set; }
    public int Stack = 0;

    public void Store(IStorable itemToStore)
    {
        if (Item == null)
        {
            Item = itemToStore;
            Stack = 1;
            return;
        }

        if (Item != null && !itemToStore.Equals(Item))
        {
            throw new InvalidOperationException("Cannot store different items to the same inventory slot");
        }

        Item = itemToStore;
        ++Stack;
    }
    
    public void RemoveItem()
    {
        --Stack;
        if (Stack == 0)
        {
            Clear();
        }
    }

    public bool IsEmpty()
    {
        return Item == null;
    }

    public void Clear()
    {
        Stack = 0;
        Item = null;
    }
}

public class Inventory : IInventory
{
    private const int InitialCapacity = 20;
    public IEnumerable<ItemSlot> Slots => itemSlots;

    private List<ItemSlot> itemSlots = new();
    private int currentCapacity;

    public event Action<ItemSlot> OnItemAdded;
    public event Action<ItemSlot> OnItemRemoved;

    public Inventory()
    {
        currentCapacity = InitialCapacity;
    }

    bool IsInventoryFull()
    {
        return itemSlots.Count() == currentCapacity;
    }

    public bool TryStore(IStorable itemToStore)
    {
        ItemSlot slot = itemSlots.FirstOrDefault(s => s.Item.Equals(itemToStore));

        if (slot == null)
        {
            if (IsInventoryFull())
            {
                return false;
            }
            slot = new ItemSlot();
            itemSlots.Add(slot);
        }

        slot.Store(itemToStore);
        OnItemAdded?.Invoke(slot);
        return true;
    }

    public void RemoveItem(IStorable storable)
    {
        var slot = itemSlots.FirstOrDefault(s => s.Item.Equals(storable));
        
        if (slot == null)
        {
            Debug.LogError("No item " + storable.Name + " found in inventory of character to remove.");
        }

        slot.RemoveItem();
        if (slot.IsEmpty())
        {
            itemSlots.Remove(slot);
        }
        OnItemRemoved?.Invoke(slot);
    }
}
