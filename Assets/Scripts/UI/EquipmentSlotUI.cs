using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class EquipmentSlotUI : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private CanvasGroup canvasGroup;

    public EquipmentSlot equipmentSlot { get; private set; }

    public Subject<EquipmentSlotUI> OnSlotHeld = new();

    private IEquipable equipment => equipmentSlot?.CurrentEquippedItem.Value;

    private IDisposable equippedItemSubscription;
    
    public void SetTo(EquipmentSlot slot)
    {
        equippedItemSubscription?.Dispose();
        equipmentSlot = slot;
        equippedItemSubscription = slot.CurrentEquippedItem.Subscribe(ItemChanged);
    }

    private void ItemChanged(IEquipable equipable)
    {
        if (equipable is null)
        {
            EmptySlot();
            return;
        }
        var item = equipable as Item;
        SetToItem(item);
    }

    private void SetToItem(Item item)
    {
        icon.sprite = item.Icon;
        canvasGroup.SetActive(true);
    }

    private void EmptySlot()
    {
        canvasGroup.SetActive(false);
    }

    public void SlotHeld()
    {
        if (equipment == null)
        {
            return;
        }
        OnSlotHeld.OnNext(this);
    }

    private void OnDestroy()
    {
        if (equipmentSlot == null)
        {
            return;
        }
        
        equippedItemSubscription?.Dispose();
        equippedItemSubscription = null;
    }
}
