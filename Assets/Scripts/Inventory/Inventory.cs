using DMT.Character.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ItemSlot
{
    public IStorable Item;
    public int Stack = 0;
    public event Action<ItemSlot> OnItemChanged;
    public event Action<ItemSlot> OnStackChanged;
    public event Action<ItemSlot> OnItemRemoved;

    public ItemSlot()
    {

    }

    public void Store(IStorable itemToStore)
    {
        if (Item == null)
        {
            Item = itemToStore;
            Stack = 1;
            OnItemChanged?.Invoke(this);
            return;
        }

        if (Item != null && itemToStore != Item)
        {
            throw new InvalidOperationException("Cannot store different items to the same inventory slot");
        }
        Item = itemToStore;
        ++Stack;
        OnStackChanged?.Invoke(this);
    }

    public void Clear()
    {
        Stack = 0;
        Item = null;
        OnItemRemoved?.Invoke(this);
    }
}

public class Inventory : IInventory
{   
    CharacterStats stats;
    
    public static int InventoryCapacity { get; private set; }
    public int NextFreeSlot { get; private set; }

    public IEnumerable<ItemSlot> Slots => inventorySlots;

    public ItemSlot[] inventorySlots;

    private const int InitialCapacity = 20;
    
    public Inventory()
    {
        inventorySlots = new ItemSlot[InitialCapacity];
        for (int i = 0; i < inventorySlots.Length; ++i)
        {
            inventorySlots[i] = new ItemSlot();
        }
        InventoryCapacity = 20;
        NextFreeSlot = 0;
    }

    ItemSlot FindItemSlot(IStorable item)
    {
        return inventorySlots.FirstOrDefault(x => x.Item != null && x.Item.Equals(item));
    }
   
    public void TryUseOnIndex(int index)
    {        
       // InventoryPickupSO item = Instance.inventorySlots[index].Item;
        
       /* if (item.Use())
        {
           
            RemoveFromInventoryOn(index);    
        }
        else
        {
            inventoryGUI.NotUsedItemEffect(index);
        }*/
    }

    public void RemoveFromInventoryOn(int index)
    {
        inventorySlots[index].Stack--;
         
        if (inventorySlots[index].Stack == 0)
        {
            DeleteItemFrominventory(index);
        }
        else
        {
            //inventoryGUI.UpdateGUIOn(index);
        }
          
    }

    void DeleteItemFrominventory(int index)
    {
        inventorySlots[index] = null;
        ShiftInventoryArray(index);
        NextFreeSlot--;
        inventorySlots[NextFreeSlot] = null;
    }

    void ShiftInventoryArray(int index)
    {
        Array.Copy(inventorySlots, index + 1, inventorySlots, index, NextFreeSlot - index - 1);       
    }

    public bool HasItemOnIndex(int index)
    {
        return inventorySlots[index] != null && inventorySlots[index].Stack > 0;
    }

    bool InventoryFull()
    {
        return NextFreeSlot  == InventoryCapacity;
    }

    private ItemSlot GetNextFreeSlot()
    {
        return inventorySlots[NextFreeSlot++];
    }

    public bool TryStore(IStorable item)
    {
        ItemSlot slot = FindItemSlot(item);

        if (slot == null)
        {
            if (InventoryFull())
            {
                return false;
            }
            slot = GetNextFreeSlot();
        }

        slot.Store(item);
        return true;
    }

    public IStorable GetItemOnSlot(int index)
    {
        return inventorySlots[index].Item;
    }
}
