using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUI : MonoBehaviour
{
    
    public static GameObject[] itemSlots;
    public static Image[] itemIcons;

    private void Awake()
    {
        
        itemSlots = new GameObject[transform.childCount];
        itemIcons = new Image[transform.childCount];

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i] = transform.GetChild(i).gameObject;
            itemIcons[i] = (itemSlots[i]).transform.GetChild(0).GetComponent<Image>();
           
            itemIcons[i].enabled = false;
        }

    }

    public static void UpdateItemGUIOn(InventoryPickupSO item)
    {
        var index = Inventory.NextFreeSlot - 1;

        Image icon = itemIcons[index];
        icon.sprite = item.Icon;
        icon.enabled = true;
        
    }
}
