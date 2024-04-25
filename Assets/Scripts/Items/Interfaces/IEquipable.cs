using DMT.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    void Equip(Character character);
    void Unequip(Character character);
}
