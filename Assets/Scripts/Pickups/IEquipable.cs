using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    EquipmentType Type { get; }
    void Equip();
    void Unequip();
}
