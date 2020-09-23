using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCharacterStats : CharacterStats
{
    [HideInInspector]
    public PlayerCharacterStatsSO stats;

    [HideInInspector]
    public float Endurance;

    [HideInInspector]
    public float Mana;
    
    protected override void Initialize()
    {
        base.Initialize();
        stats = (PlayerCharacterStatsSO)Data;
        Endurance = stats.MaxEndurace;
        Mana = stats.MaxMana;
    }

    public bool HasMaxEndurance()
    {
        return stats.MaxEndurace == Endurance;
    }

    public bool HasMaxMana()
    {
        return stats.MaxMana == Mana;
    }

    public bool HasEnduranceForAttack()
    {
        return Endurance >= stats.EndurancePerAttack;
    }
}
