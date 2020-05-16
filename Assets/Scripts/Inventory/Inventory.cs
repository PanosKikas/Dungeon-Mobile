using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    #region StoredItemData
    [System.Serializable]
    public class StoredItem
    {
        public InventoryPickupSO Item;
        public int Stack = 0;

        public StoredItem(InventoryPickupSO _item)
        {
            Item = _item;
            Stack = 1;
        }
    }
    #endregion
    
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
        stats = (MainHeroPlayerStats)StatsDatabase.Instance.PlayerCharacterStats[0];
        items = new StoredItem[stats.InventorySpace];
        InventoryCapacity = stats.InventorySpace;
        NextFreeSlot = 0;
    }

    public void StoreToInventory(InventoryPickupSO item)
    {
        int? found =  ItemExistsInventory(item);

        int insertedIndex = found ?? (NextFreeSlot++);
        InsertItemOn(item, insertedIndex, found.HasValue);
        inventoryGUI.ModifyItemGUIOn(insertedIndex);
        
    }
    
    void InsertItemOn(InventoryPickupSO item, int index, bool found)
    {
        if (found)
        {
            Instance.items[index].Stack++;
        }
        else
        {
            
            Instance.items[index] = new StoredItem(item);
        }

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
            inventoryGUI.NotUsedItem(index);
        }
    }

    void RemoveFromInventoryOn(int index)
    {
        items[index].Stack--;
        if (items[index].Stack == 0)
        {
            items[index] = null;
        }
        
        inventoryGUI.ModifyItemGUIOn(index);
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
}
