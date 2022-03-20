using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffects : StatusEffects
{
    
    public static void DecreaseEndurance(PlayerCharacterStats playerStats)
    {
        playerStats.Endurance = Mathf.Clamp(playerStats.Endurance - playerStats.stats.EndurancePerAttack,
                                            0, playerStats.stats.MaxEndurace);
    }

    public static void RechargeEndurance(PlayerCharacterStats playerStats)
    {
        float endurance = Time.deltaTime * playerStats.stats.EnduranceRechargeRate;
        playerStats.Endurance = Mathf.Clamp(playerStats.Endurance + endurance, 0f, playerStats.stats.MaxEndurace);
    }

}
