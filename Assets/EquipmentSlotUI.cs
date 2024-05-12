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

    private EquipmentSlot slot;

    public void SubscribeTo(EquipmentSlot slot)
    {
        this.slot = slot;
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

    private void OnDestroy()
    {
        if (slot == null)
        {
            return;
        }
        slot.OnItemChanged -= ItemEquipped;
    }
}
