using DMT.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item, IUsable, IEquipable
{
    private EquipmentData data;

    public override string Name => data.Name;

    public override string Description => data.Description;

    public override Sprite Icon => data.Icon;

    public EquipmentType EquipmentType => data.EquipmentType;

    public Equipment(EquipmentData equipableSO)
    {
        this.data = equipableSO;
    }

    public void EquipOn(Character character)
    {
        foreach (var modifier in data.Modifiers)
        {
            var stat = character.stats.GetStatOfType(modifier.StatType);
            stat.AddModifier(new StatModifier(modifier.Value, this));
        }
    }

    public void UnequipFrom(Character character)
    {
        
    }

    public bool TryUseOn(Character character)
    {
        character.Equip(this);
        return true;
    }
}
