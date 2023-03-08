using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName = "Characters/Stats")]
public class CharacterStatsSO : ScriptableObject
{
    public int MaxHealth = 100;
    public float PhysicalDamage = 10;
    public float AutoAttackRate = 1f;
    public int MaxStamina = 100;
    public int MaxDivnity = 100;
    public float StaminaRegen = 1f;
    public int DivineDamage = 10;
    public float CriticalChance = 0;
    public int PhysicalDefense = 10;
    public int DivinityResistance = 10;
    public float EvasionChance = 0f;
    public float ItemDropRate = 0f;
    public float CooldownReduction = 0f;
}
