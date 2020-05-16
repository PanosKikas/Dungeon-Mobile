using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryGUI : MonoBehaviour
{
    public ItemSlotUI[] itemSlots;

    [SerializeField]
    Text itemDescritpion;

    int? lastDisplayedIndex = null;

    private void Start()
    {
        InitializeUI();
    }

    void InitializeUI()
    {
        itemSlots = new ItemSlotUI[transform.childCount];
        itemSlots = GetComponentsInChildren<ItemSlotUI>();      
    }

    public void OnEnable()
    {
        for (int i = 0; i < Inventory.InventoryCapacity; ++i)
        {
            UpdateGUIOn(i);
        }   
    }

    public void UpdateGUIOn(int index)
    {
        if (Inventory.Instance.HasItemOnIndex(index))
        {
            ShowItemOn(index);
        }
        else
        {
            HideItemFrom(index);
        }
    }

    public void DisplayItemOnDescription(int index)
    {
        InventoryPickupSO item = Inventory.Instance.items[index].Item;
        DisplayItemDescription(item);
        lastDisplayedIndex = index;
    }

    private void DisplayItemDescription(InventoryPickupSO item)
    {
        itemDescritpion.text = string.Format("{0}: {1}", item.Name, item.description);
    }

    public void NotUsedItemEffect(int index)
    {
        itemSlots[index].CannotBeUsedAnimate();
    }
    
    private void ShowItemOn(int index)
    {
        var itemEntry = Inventory.Instance.items[index];
        itemSlots[index].ShowItemOnSlot(itemEntry);
    }

    private void HideItemFrom(int index)
    {
        itemSlots[index].RemoveItemFromSlot();

        if (DescriptionShowingRemovedItem(index))
        {
            HideDescriptionText();
        }
    }
    
    bool DescriptionShowingRemovedItem(int index)
    {
        return lastDisplayedIndex != null && lastDisplayedIndex == index;
    }

    void HideDescriptionText()
    {
        itemDescritpion.text = "";
    }
    
}
