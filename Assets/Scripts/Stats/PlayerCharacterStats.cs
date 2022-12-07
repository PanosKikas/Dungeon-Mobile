using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterStats : CharacterStats
{
    public float Endurance;
    public float Mana;

    private PlayerCharacterStatsSO playerInitialStats => InitialData as PlayerCharacterStatsSO;

    public float EnduranceRechargeRate => playerInitialStats.EnduranceRechargeRate;
    public float ManualAttackRate => playerInitialStats.ManualAttackRate;
    public int MaxEndurance => playerInitialStats.MaxEndurace;
    
    public PlayerCharacterStats(PlayerCharacterStatsSO initialData) : base(initialData)
    {
        initialData.Initialize();
        Endurance = initialData.MaxEndurace;
        Mana = initialData.MaxMana;
    }

    public void PerformAttack()
    {
        Endurance = Mathf.Clamp(Endurance - playerInitialStats.EndurancePerAttack,
            0, playerInitialStats.MaxEndurace);
    }

    public bool HasEnduranceForAttack()
    {
        return Endurance >= playerInitialStats.EndurancePerAttack;
    }
}
