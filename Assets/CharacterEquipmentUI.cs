using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipmentUI : MonoBehaviour
{

    ArmorSlotUI[] armorSlots;

    private void OnEnable()
    {
        if (armorSlots == null || armorSlots.Length == 0)
        {
            FindArmorSlots();
        }

        foreach (var armorSlot in armorSlots)
        {
            armorSlot.UpdateArmorSlot();
        }

    }

    void FindArmorSlots()
    {
        armorSlots = GetComponentsInChildren<ArmorSlotUI>();
    }
}
