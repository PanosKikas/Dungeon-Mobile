using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{

    MainHeroPlayerStats stats;
    public static int InventoryCapacity { get; private set; }
    public static int NextFreeSlot { get; set; }
    public static List<InventoryPickupSO> items;
    

    private void Awake()
    {
        stats = (MainHeroPlayerStats)GetComponent<PlayerStatusEffects>().stats;
        items = new List<InventoryPickupSO>();

    }

    private void Start()
    {
        InventoryCapacity = stats.InventorySpace;
        NextFreeSlot = 0;
    }

    public static void StoreToInventory(InventoryPickupSO item)
    {
        items.Add(item);
        NextFreeSlot++;
        InventoryGUI.UpdateItemGUIOn(item);
    }
    

    public static void RemoveFromInventory(InventoryPickupSO item)
    {

    }



}
