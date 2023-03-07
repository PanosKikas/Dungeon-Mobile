using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CharacterStats 
{
    public Stat MaxHealth { get; }
    public Stat PhysicalDamage { get; }
    public Stat AutoAttackRate { get; }
    public Stat MaxStamina { get; }
    public Stat MaxDivnity { get; }
    public Stat StaminaRegen { get; }
    public Stat EvasionChance { get; }
    public Stat DivineDamage { get; }
    public Stat CriticalChance { get; }
    public Stat PhysicalDefense { get; }
    public Stat DivinityResistance { get; }
    public Stat ItemDropRate { get; }
    public Stat CooldownReduction { get; }


    public CharacterStats(CharacterStatsSO initialValues)
    {
        MaxHealth = new Stat(initialValues.MaxHealth);
        PhysicalDamage = new Stat(initialValues.PhysicalDamage);
        AutoAttackRate = new Stat(initialValues.AutoAttackRate);
        MaxStamina = new Stat(initialValues.MaxStamina);
        MaxDivnity = new Stat(initialValues.MaxDivnity);
        StaminaRegen = new Stat(initialValues.StaminaRegen);
        DivineDamage = new Stat(initialValues.DivineDamage);
        EvasionChance = new Stat(initialValues.EvasionChance);
        CriticalChance = new Stat(initialValues.CriticalChance);
        PhysicalDefense = new Stat(initialValues.PhysicalDefense);
        DivinityResistance = new Stat(initialValues.DivinityResistance);
        ItemDropRate = new Stat(initialValues.ItemDropRate);
        CooldownReduction = new Stat(initialValues.CooldownReduction);
    }
}
