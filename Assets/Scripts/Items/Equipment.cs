using DMT.Characters;
using DMT.Characters.Stats;
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

    private Dictionary<StatType, StatModifier> modifiersAdded = new();

    public Equipment(EquipmentData equipableSO)
    {
        this.data = equipableSO;
    }

    public void EquipOn(Character character)
    {
        modifiersAdded.Clear();
        foreach (var modifier in data.Modifiers)
        {
            var characterStat = character.stats.GetStatOfType(modifier.StatType);
            var statModifier = new StatModifier(modifier.Value, this);
            characterStat.AddModifier(statModifier);
            modifiersAdded.Add(modifier.StatType, statModifier);
        }
    }

    public void UnequipFrom(Character character)
    {
        foreach (var kvp in modifiersAdded)
        {
            var characterStat = character.stats.GetStatOfType(kvp.Key);
            characterStat.RemoveModifier(kvp.Value);
        }
        modifiersAdded.Clear();
    }

    public bool TryUseOn(Character character)
    {
        character.Equip(this);
        return true;
    }
}
