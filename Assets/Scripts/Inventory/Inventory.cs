using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemEvent : UnityEvent<StoredItem> { }

#region StoredItemData
[System.Serializable]
public class StoredItem 
{
    public Item Item;
    public int Stack = 0;
    public StoredItem(Item _item)
    {
        Item = _item;
        Stack = 0;
    }
}
#endregion
public class Inventory : MonoBehaviour
{
    public static int InventoryCapacity { get; private set; }
    public int NextFreeSlot { get; private set; }
    public StoredItem[] items;

    [SerializeField]
    InventoryGUI inventoryGUI;

    [SerializeField]
    UIToggler toggler;

    #region Singletton
    public static Inventory Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
 
    }
    #endregion
    
    private void Start()
    {
        InitializeInventory();
    }

    void InitializeInventory()
    {
        items = new StoredItem[20];
        InventoryCapacity = 20;
        NextFreeSlot = 0;
    }

    public bool TryStore(Item item)
    {
        int? found = ItemExistsInventory(item);
        //int indexToInsert = FindIndexToInsert(item);
        int insertedIndex;
        if (found == null)
        {
            if (InventoryFull())
            {
                return false;
            }
            else
            {
                insertedIndex = NextFreeSlot++;
                CreateNewItemOn(item, insertedIndex);
            }
        }
        else
        {
            insertedIndex = found.Value;
        }

        items[insertedIndex].Stack++;
        inventoryGUI.UpdateGUIOn(insertedIndex);
        return true;
    }
    
    int? ItemExistsInventory(Item itemToFind)
    {
        for (int i = 0; i < NextFreeSlot; ++i)
        {
            if (items[i].Item.Equals(itemToFind))
            {
                return i;
            }
        }
        return null;
    }

    void CreateNewItemOn(Item item, int index)
    {
        Instance.items[index] = new StoredItem(item);       
    }
   
    public void TryUseOnIndex(int index)
    {        
        Item item = Instance.items[index].Item;
        IUsable usable = item as IUsable;
        if (usable is null)
        {
            return;
        }
        
        if (usable.TryUse())
        {
            RemoveFromInventoryOn(index);    
        }
        else
        {
            inventoryGUI.NotUsedItemEffect(index);
        }
    }

    public void RemoveFromInventoryOn(int index)
    {
        items[index].Stack--;
         
        if (items[index].Stack == 0)
        {
            DeleteItemFrominventory(index);
        }
        else
        {
            inventoryGUI.UpdateGUIOn(index);
        }
          
    }

    void DeleteItemFrominventory(int index)
    {
        items[index] = null;
        inventoryGUI.UpdateGUIOn(index);
        ShiftInventoryArray(index);
        NextFreeSlot--;
        items[NextFreeSlot] = null;
        toggler.ToggleInventory();
        toggler.ToggleInventory();
    }

    void ShiftInventoryArray(int index)
    {
        Array.Copy(items, index + 1, items, index, NextFreeSlot - index - 1);       
    }

    public bool HasItemOnIndex(int index)
    {
        return items[index] != null && items[index].Stack > 0;
    }

    bool InventoryFull()
    {
        return NextFreeSlot  == InventoryCapacity;
    }
}
