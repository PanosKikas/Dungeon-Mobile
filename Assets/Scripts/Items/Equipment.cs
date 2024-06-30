using DMT.Characters;
using DMT.Characters.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item, IUsable, IEquipable
{
    private readonly EquipmentData data;

    private int minEquipLevel => data.MinLevel;

    private CharacterClass requiredCharacterClass => data.CharacterClass;
    
    public override string Name => data.Name;

    public override string Description => data.Description;

    public override Sprite Icon => data.Icon;

    public EquipmentType EquipmentType => data.EquipmentType;

    private readonly Dictionary<StatType, StatModifier> modifiersAdded = new();

    public Equipment(EquipmentData equipableSO)
    {
        data = equipableSO;
    }

    public void EquipOn(Character character)
    {
        modifiersAdded.Clear();
        foreach (var modifier in data.Modifiers)
        {
            var characterStat = character.Stats.GetStatOfType(modifier.StatType);
            var statModifier = new StatModifier(modifier.Value);
            characterStat.AddModifier(statModifier);
            modifiersAdded.Add(modifier.StatType, statModifier);
        }
    }

    public void UnequipFrom(Character character)
    {
        foreach (var kvp in modifiersAdded)
        {
            var characterStat = character.Stats.GetStatOfType(kvp.Key);
            characterStat.RemoveModifier(kvp.Value);
        }
        modifiersAdded.Clear();
    }

    public bool CanBeUsedOn(Character character)
    {
        return SatisfiesClassRequirement(character) && SatisfiesMinLevel(character);
    }

    private bool SatisfiesClassRequirement(Character character)
    {
        if (requiredCharacterClass == CharacterClass.Any)
        {
            return true;
        }

        return character.CharacterClass == requiredCharacterClass;
    }

    private bool SatisfiesMinLevel(Character character)
    {
        return character.Level.Value >= minEquipLevel;
    }

    public void UseOn(Character character)
    {
        character.Equip(this);
    }
}
