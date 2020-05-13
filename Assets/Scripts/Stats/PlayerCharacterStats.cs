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

    protected override void Initialize()
    {
        base.Initialize();
        CurrentEndurance = MaxEndurace;
    }
}
