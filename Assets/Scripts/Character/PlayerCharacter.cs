using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCharacter : Character
{
    [HideInInspector]
    public PlayerCharacterStatsSO stats;

    public PlayerCharacterStats playerStats => characterStats as PlayerCharacterStats;
    
    protected override void Initialize()
    {
        base.Initialize();
        stats = (PlayerCharacterStatsSO)Data;
    }

    public bool HasMaxEndurance()
    {
        return stats.MaxEndurace == playerStats.Endurance;
    }

    public bool HasMaxMana()
    {
        return stats.MaxMana == playerStats.Mana;
    }

    public bool HasEnduranceForAttack()
    {
        return playerStats.Endurance >= stats.EndurancePerAttack;
    }

    protected override CharacterStats CreateStats()
    {
        return new PlayerCharacterStats(stats);
    }
}
