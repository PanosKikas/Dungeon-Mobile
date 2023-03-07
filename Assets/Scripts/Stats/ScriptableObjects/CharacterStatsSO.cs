using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class CharacterStatsSO : ScriptableObject
{
    public int MaxHealth;
    public float PhysicalDamage;
    public float AutoAttackRate;
    public int MaxStamina;
    public int MaxDivnity;
    public float StaminaRegen;
    public int DivineDamage;
    public float CriticalChance;
    public int PhysicalDefense;
    public int DivinityResistance;
    public float EvasionChance;
    public float ItemDropRate;
    public float CooldownReduction;
}
