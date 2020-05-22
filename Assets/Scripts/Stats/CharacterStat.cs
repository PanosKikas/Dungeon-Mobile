using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
public enum Stat
{
    MaxHealth,
    MaxEndurance,
    EnduranceRegen,
    MaxMana,
    AttackDamage,
    AttackSpeed,
    CriticalDamage,
    EvasionChance,
    CriticalChance,
    ItemDrop,
    MagicDamage,
    PhysicalArmor,
    MagicResist

}

[System.Serializable]
public class CharacterStat
{
    private bool isDirty = true;
    public float BaseValue;
    private float _value;
    private float lastBaseValue = float.MinValue;

    public Stat Type;

    public float Value
    {
        get
        {
            /*if (isDirty || (BaseValue != lastBaseValue))
            {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }*/
            
            return CalculateFinalValue();
        }
    }
    
    private readonly List<StatModifier> statModifiers;

    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    public CharacterStat() 
    {
        isDirty = true;
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
    }


    public bool RemoveModifier(StatModifier mod)
    {
        
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    public bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;
        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    private float CalculateFinalValue()
    {
        var finalValue = BaseValue;
        for (int i = 0; i < statModifiers.Count; ++i)
        {
            StatModifier modifier = statModifiers[i];
            
            finalValue += modifier.Value;
           
        }
        return (float)Math.Round(finalValue, 4);
    }

}
