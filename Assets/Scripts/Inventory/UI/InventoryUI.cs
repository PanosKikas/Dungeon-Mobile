using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DMT.Characters;
using System.Linq;

public class InventoryUI : MonoBehaviour
{
    private Dictionary<ItemSlot, ItemSlotUI> usedSlots = new();
    private List<ItemSlotUI> freeSlots = new();

    [SerializeField]
    private Transform slotsParent;

    [SerializeField]
    Text itemDescritpion;

    private IInventory inventory;
    private Character selectedCharacter => characters.First();
    private ItemSlotUI currentSelectedSlot;
    private IEnumerable<Character> characters;

    private void Awake()
    {
        freeSlots = slotsParent.GetComponentsInChildren<ItemSlotUI>().ToList();
    }

    public void Initialize(IInventory inventory, IEnumerable<Character> characters)
    {
        if (inventory == null)
        {
            Debug.LogError("Character does not contain an inventory");
            return;
        }
        this.inventory = inventory;
        this.characters = characters;
        SubscribeTo(inventory);
    }

    private void SubscribeTo(IInventory inventory)
    {
        inventory.OnItemAdded += OnItemAdded;
        inventory.OnItemRemoved += OnItemRemoved;
    }

    private void OnItemAdded(ItemSlot slot)
    {
        if (usedSlots.TryGetValue(slot, out var slotUI))
        {
            slotUI.UpdateUI();
        }
        else
        {
            var slotUIToUse = freeSlots.First();
            freeSlots.Remove(slotUIToUse);
            slotUIToUse.InitializeTo(slot);
            usedSlots.Add(slot, slotUIToUse);
            SubscribeToItemSlotEvents(slotUIToUse);
            slotUIToUse.transform.SetSiblingIndex(usedSlots.Count() - 1);
        }
    }

    private void OnItemRemoved(ItemSlot slot)
    {
        var slotUI = usedSlots[slot];
        if (slot.IsEmpty())
        {
            if (currentSelectedSlot == slotUI)
            {
                EmptyDescriptionText();
            }
            UnsubscribeFromItemSlotEvents(slotUI);
            slotUI.Empty();
            freeSlots.Add(slotUI);
            usedSlots.Remove(slot);
            slotUI.transform.SetSiblingIndex(usedSlots.Count() + 1);
        }
        else
        {
            slotUI.UpdateUI();
        }
    }

    private void SubscribeToItemSlotEvents(ItemSlotUI itemSlotUi)
    {
        itemSlotUi.OnClicked += OnItemSlotClicked;
        itemSlotUi.OnHeld += OnItemSlotHeld;
    }

    private void UnsubscribeFromItemSlotEvents(ItemSlotUI itemSlotUI)
    {
        itemSlotUI.OnClicked -= OnItemSlotClicked;
        itemSlotUI.OnHeld -= OnItemSlotHeld;
    }

    private void OnItemSlotClicked(ItemSlotUI itemSlotUI)
    {
        if (itemSlotUI.Item == null)
        {
            throw new System.InvalidOperationException("Item slot clicked is null");
        }

        currentSelectedSlot = itemSlotUI;
        ShowItemDescription(itemSlotUI.Item);
    }

    private void OnItemSlotHeld(ItemSlotUI itemSlotUI)
    {
        if (itemSlotUI.Item == null)
        {
            throw new System.InvalidOperationException("Item slot held is null");
        }

        if (itemSlotUI.Item is IUsable usable && usable.TryUseOn(selectedCharacter))
        {
            inventory.RemoveItem(itemSlotUI.Item);
        }
        else
        {
            itemSlotUI.Shake();
        }
    }

    private void ShowItemDescription(IStorable item)
    {
        if (item == null)
        {
            EmptyDescriptionText();
            return;
        }
        itemDescritpion.text = string.Format("{0}: {1}", item.Name, item.Description);
    }

    private void EmptyDescriptionText()
    {
        currentSelectedSlot = null;
        itemDescritpion.text = string.Empty;
    }

    private void OnDestroy()
    {
        inventory.OnItemAdded -= OnItemAdded;
        inventory.OnItemRemoved -= OnItemRemoved;
    }
}
