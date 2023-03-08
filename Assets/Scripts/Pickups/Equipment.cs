using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlotType
{
    Head,
    Chest,
    Legs,
    Weapon
}

public class Equipment : Item, IEquipable, IStorable, IUsable
{
    public EquipmentSlotType SlotType { get; private set; }

    public Equipment(PickupSO data) : base(data)
    {
        EquipmentSO equipmentData = data as EquipmentSO;
        if (equipmentData != null)
        {
            SlotType = equipmentData.slotType;
        }
    }

    public void Equip(Character character)
    {
        character.Equipment.Equip(this);
    }

    public void Unequip()
    {
    }
    

    public bool TryUseOn(Character character)
    {
        Equip(character);
        return true;
    }
}