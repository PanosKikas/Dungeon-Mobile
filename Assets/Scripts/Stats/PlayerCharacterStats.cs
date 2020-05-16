using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Stats/PlayerStats")]
public class PlayerCharacterStats : CharacterStats
{
    public float ManualAttackRate = 2f;
    [HideInInspector]
    public float CurrentEndurance;
    public int MaxEndurace = 100;

    public float EndurancePerAttack = 4;

    public float EnduranceRechargeRate = 1f;

    public int MaxMana = 200;
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
}
