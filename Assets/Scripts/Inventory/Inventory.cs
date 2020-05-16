﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemEvent : UnityEvent<StoredItem> { }

#region StoredItemData
[System.Serializable]
public class StoredItem 
{
    public InventoryPickupSO Item;
    public int Stack = 0;
    public StoredItem(InventoryPickupSO _item)
    {
        Item = _item;
        Stack = 0;
    }
}
#endregion
public class Inventory : MonoBehaviour
{   
    MainHeroPlayerStats stats;
    public static int InventoryCapacity { get; private set; }
    public int NextFreeSlot { get; set; }
    public StoredItem[] items;

    [SerializeField]
    InventoryGUI inventoryGUI;

    

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
        stats = (MainHeroPlayerStats)StatsDatabase.Instance.PlayerCharacterStats[0];
        items = new StoredItem[stats.InventorySpace];
        InventoryCapacity = stats.InventorySpace;
        NextFreeSlot = 0;
    }

    public void StoreToInventory(InventoryPickupSO item)
    {
        int indexToInsert = FindIndexToInsert(item);
        
        if (items[indexToInsert] == null || items[indexToInsert].Item == null)
        {
            CreateNewItemOn(item, indexToInsert);
        }
        items[indexToInsert].Stack++;
        inventoryGUI.ShowItemOn(indexToInsert);

    }
    

    int FindIndexToInsert(InventoryPickupSO item)
    {
        int? found = ItemExistsInventory(item);
        return found ?? (NextFreeSlot++);
       
    }

    int? ItemExistsInventory(InventoryPickupSO itemToFind)
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

    void CreateNewItemOn(InventoryPickupSO item, int index)
    {
        Instance.items[index] = new StoredItem(item);
        
    }
   
    public void TryUseOnIndex(int index)
    {
        
        InventoryPickupSO item = Instance.items[index].Item;
        
        if (item.Use())
        {
            
            RemoveFromInventoryOn(index);    
        }
        else
        {
            inventoryGUI.NotUsedItemEffect(index);
        }
    }

    void RemoveFromInventoryOn(int index)
    {
        items[index].Stack--;
         
        if (items[index].Stack == 0)
        {
            NextFreeSlot--;
            items[index] = null;
            inventoryGUI.HideItemFrom(index);
        }
        else
        {
            inventoryGUI.ShowItemOn(index);
        }
    }


}
