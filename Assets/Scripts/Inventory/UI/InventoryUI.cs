using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DMT.Character;
using System.Linq;

public class InventoryUI : MonoBehaviour
{
    private ItemSlotUI[] itemSlotsUI;

    [SerializeField]
    Text itemDescritpion;

    [SerializeField]
    private Character character;

    private void Awake()
    {
        itemSlotsUI = GetComponentsInChildren<ItemSlotUI>();
    }

    private void Start()
    {
        var inventorySlots = character.inventory.Slots.ToArray();

        if (inventorySlots.Length != itemSlotsUI.Length)
        {
            Debug.LogError("Inventory slots has different length than the item slots UI");
            return;
        }

        for (int i = 0; i < itemSlotsUI.Length; ++i)
        {
            itemSlotsUI[i].SubscribeToSlot(inventorySlots[i]);
            itemSlotsUI[i].OnClicked += OnItemSlotClicked;
            itemSlotsUI[i].OnHeld += OnItemSlotHeld;
        }
    }

    private void OnItemSlotClicked(ItemSlot itemSlot)
    {
        if (itemSlot == null)
        {
            throw new System.InvalidOperationException("Item slot clicked is null");
        }
        DisplayItemDescription(itemSlot.Item);
    }

    private void OnItemSlotHeld(ItemSlot itemSlot)
    {
        if (itemSlot == null)
        {
            throw new System.InvalidOperationException("Item slot held is null");
        }

        if (itemSlot.Item is IUsable usable)
        {
            if (usable.TryUseOn(character))
            {
                character.RemoveFromInventory(itemSlot.Item);
            }
        }
    }

    /*    public void OnEnable()
        {
            if (itemSlots == null || itemSlots.Length == 0)
            {
                InitializeItemSlots();
            }
            lastDisplayedIndex = null;
            HideDescriptionText();

            for (int i = 0; i < Inventory.InventoryCapacity; ++i)
            {
                UpdateGUIOn(i);
            }   
        }*/

    private void DisplayItemDescription(IStorable item)
    {
        itemDescritpion.text = string.Format("{0}: {1}", item.Name, item.Description);
    }

    public void NotUsedItemEffect(int index)
    {
        itemSlotsUI[index].CannotBeUsedAnimate();
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
