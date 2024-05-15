using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private CanvasGroup canvasGroup;

    public EquipmentSlot equipmentSlot { get; private set; }

    public Action<EquipmentSlotUI> OnSlotHeld;

    private IEquipable equipment => equipmentSlot?.CurrentEquippedItem;

    public void SubscribeTo(EquipmentSlot slot)
    {
        this.equipmentSlot = slot;
        ItemEquipped(slot.CurrentEquippedItem);
        slot.OnItemChanged += ItemEquipped; 
    }

    private void ItemEquipped(IEquipable equipment)
    {
        if (equipment is null)
        {
            EmptySlot();
            return;
        }
        var item = equipment as Item;
        SetToItem(item);
    }

    public void SetToItem(Item item)
    {
        icon.sprite = item.Icon;
        canvasGroup.SetActive(true);
    }

    public void EmptySlot()
    {
        canvasGroup.SetActive(false);
    }

    public void SlotHeld()
    {
        if (equipment == null)
        {
            return;
        }
        OnSlotHeld?.Invoke(this);
    }

    private void OnDestroy()
    {
        if (equipmentSlot == null)
        {
            return;
        }
        equipmentSlot.OnItemChanged -= ItemEquipped;
    }
}
