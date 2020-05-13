using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    MainHeroPlayerStats stats;
    public int InventoryCapacity { get; private set; }
    public int FreeInventorySlots { get; set; }

    private void Awake()
    {
        stats = (MainHeroPlayerStats)GetComponent<PlayerStatusEffects>().stats;
        

    }

    private void Start()
    {
        InventoryCapacity = stats.InventorySpace;
        FreeInventorySlots = InventoryCapacity;
    }

    public void StoreToInventory()
    {

    }




}
