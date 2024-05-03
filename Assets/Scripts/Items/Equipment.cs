using DMT.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item, IUsable
{
    private EquipmentData data;

    public override string Name => data.Name;

    public override string Description => data.Description;

    public override Sprite Icon => data.Icon;

    public Equipment(EquipmentData equipableSO)
    {
        this.data = equipableSO;
    }

    public void Equip(Character character)
    {
        throw new System.NotImplementedException();
    }

    public void Unequip(Character character)
    {
        throw new System.NotImplementedException();
    }

    public void TryUseOn(Character character)
    {
        
    }
}
