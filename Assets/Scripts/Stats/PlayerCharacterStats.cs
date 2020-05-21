using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Stats/PlayerStats")]
public class PlayerCharacterStats : CharacterStats
{

    public CharacterStat ManualAttackRateStat = new CharacterStat(Stat.AttackSpeed);
    public CharacterStat MaxEnduranceStat = new CharacterStat(Stat.MaxEndurance);
    public CharacterStat MaxManaStat = new CharacterStat(Stat.MaxMana);
    public CharacterStat EnduranceRegenStat = new CharacterStat(Stat.EnduranceRegen);

    public float ManualAttackRate
    {
        get
        {
            return ManualAttackRateStat.BaseValue;
        }
        set
        {
            ManualAttackRateStat.BaseValue = value;
        }
    }
     
    public float CurrentEndurance;
    public int MaxEndurace
    {
        get
        {
            return (int)MaxEnduranceStat.BaseValue;
        }
        set
        {
            MaxEnduranceStat.BaseValue = value;
        }
    }

    public float EndurancePerAttack = 4;

    public float EnduranceRechargeRate
    {
        get
        {
            return EnduranceRegenStat.BaseValue;
        }
        set
        {
            EnduranceRegenStat.BaseValue = value;
        }
    }

    public int MaxMana
    {
        get
        {
            return (int)MaxManaStat.BaseValue;
        }
        set
        {
            MaxManaStat.BaseValue = value;
        }
    }

    public int CurrentMana;

    protected override void Initialize()
    {
        base.Initialize();
        CurrentEndurance = MaxEndurace;
        CurrentMana = MaxMana;
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
