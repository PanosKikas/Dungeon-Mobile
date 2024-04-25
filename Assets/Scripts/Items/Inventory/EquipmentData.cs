using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EquipmentType
{
    Head,
    Chest,
    Legs,
    Weapon,
    Potion
}

public class EquipmentData : ScriptableObject
{
    public string Name;
    public string Description;
    public EquipmentType EquipmentType;
    public Sprite Icon;

    public void Equip()
    {
        CharacterEquipment.Instance.Equip(this);
    }

    public virtual void Unequip() { }
 
}
