using DMT.Characters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPanel : MonoBehaviour
{
    private Character character;
    private EquipmentSlotUI[] equipmentSlotsUI;

    private void Awake()
    {
        equipmentSlotsUI = GetComponentsInChildren<EquipmentSlotUI>();
    }

    public void SubscribeTo(Character character)
    {
        this.character = character;
        var equipment = character.Equipment;
        for (int i = 0; i < equipment.EquipmentSlots.Length; ++i)
        {
            var slot = equipment.EquipmentSlots[i];
            equipmentSlotsUI[i].SubscribeTo(slot);
            equipmentSlotsUI[i].OnSlotHeld += SlotHeld;
        }
    }

    private void SlotHeld(EquipmentSlotUI slotUI)
    {
        character.UnequipFrom(slotUI.equipmentSlot);
    }

    private void OnDestroy()
    {
        foreach (var equipmentSlotUI in equipmentSlotsUI)
        {
            equipmentSlotUI.OnSlotHeld -= SlotHeld;
        }
    }
}
