using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Stats/PlayerStats")]
public class PlayerCharacterStats : CharacterStats
{

    public CharacterStat ManualAttackRateStat;
    public CharacterStat MaxEnduranceStat;
    public CharacterStat MaxManaStat;
    public CharacterStat EnduranceRegenStat;
    public CharacterStat CriticalDamageStat;
    public CharacterStat CriticalChanceStat;
    public CharacterStat MagicDamageStat;
    public CharacterStat EvasionChanceStat;
    public CharacterStat PhysicalDefenseStat;
    public CharacterStat MagicalResistanceStat;
    public CharacterStat ItemDropRateStat;

    public float ManualAttackRate
    {
        get
        {
            return ManualAttackRateStat.Value;
        }

    }
     
    public float CurrentEndurance;
    public int MaxEndurace
    {
        get
        {
            return (int)MaxEnduranceStat.Value;
        }

    }

    public float EndurancePerAttack = 4;

    public float EnduranceRechargeRate
    {
        get
        {
            return EnduranceRegenStat.Value;
        }
    }

    public int MaxMana
    {
        get
        {
            return (int)MaxManaStat.Value;
        }
      
    }

    public int CurrentMana;

    public override void Initialize()
    {
        base.Initialize();
        CurrentEndurance = MaxEndurace;
        CurrentMana = MaxMana;

        upgradableStatsList.Add(ManualAttackRateStat);
        upgradableStatsList.Add(MaxEnduranceStat);
        upgradableStatsList.Add(MaxManaStat);
        upgradableStatsList.Add(EnduranceRegenStat);
        upgradableStatsList.Add(CriticalDamageStat);
        upgradableStatsList.Add(CriticalChanceStat);
        upgradableStatsList.Add(MagicDamageStat);
        upgradableStatsList.Add(EvasionChanceStat);
        upgradableStatsList.Add(PhysicalDefenseStat);
        upgradableStatsList.Add(MagicalResistanceStat);
        upgradableStatsList.Add(ItemDropRateStat);
    }

    public bool HasMaxEndurance()
    {
        return MaxEndurace == CurrentEndurance;
    }

    public bool HasMaxMana()
    {
        return MaxMana == CurrentMana;
    }

    public bool HasEndurance()
    {
        return CurrentEndurance >= EndurancePerAttack;
    }
}
