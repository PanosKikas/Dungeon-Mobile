using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    EquipmentSlotType SlotType { get; }
    void Equip(Character character);
    void Unequip();
}
