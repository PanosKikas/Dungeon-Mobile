using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Head,
    Chest,
    Legs,
    Weapon
}

public class Equipment : Item, IEquipable, IStorable, IUsable
{
    public EquipmentType Type => EquipmentType.Chest;

    public void Equip()
    {
        
    }

    public void Unequip()
    {
        
    }
    

    public void Use()
    {
        Equip();
    }

    public bool TryUse()
    {
        Equip();
        return true;
    }
}
