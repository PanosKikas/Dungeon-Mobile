using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipment
{
    public readonly Dictionary<EquipmentSlotType, IEquipable> Equipment = new();
    
    
    public void Equip(IEquipable item)
    {
        var slotType = item.SlotType;

        if (!Equipment.ContainsKey(slotType))
        {
            Equipment.Add(slotType, null);
        }
        else
        {
            Equipment[slotType].Unequip();
        }

        Equipment[slotType] = item;
    }
}
