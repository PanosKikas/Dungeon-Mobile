using DMT.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable 
{
    void EquipOn(Character character);
    void UnequipFrom(Character character);
    EquipmentType EquipmentType { get; }
}
