using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffects : StatusEffects
{
    PlayerCharacterStats playerStats;

    private void Awake()
    {
        playerStats = (PlayerCharacterStats)stats;
    }

    public void DecreaseEndurance()
    {
        playerStats.CurrentEndurance = Mathf.Clamp(playerStats.CurrentEndurance - playerStats.EndurancePerAttack,
                                            0, playerStats.MaxEndurace);
    }

    private void Update()
    {
        RechargeEndurance(Time.deltaTime * playerStats.EnduranceRechargeRate);
    }

    void RechargeEndurance(float endurance)
    {
        playerStats.CurrentEndurance = Mathf.Clamp(playerStats.CurrentEndurance + endurance, 0f, playerStats.MaxEndurace);
    }

}
