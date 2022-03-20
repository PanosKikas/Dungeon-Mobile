using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnduranceUI : BattleUI
{
    private PlayerCharacterStats playerStats;

    protected override void Start()
    {
        base.Start();
        playerStats = (PlayerCharacterStats)stats;

    }
    private void Update()
    {
        UpdateEnduranceBar();
    }
    public void UpdateEnduranceBar()
    {

        bar.value = (float)playerStats.Endurance / (float)playerStats.stats.MaxEndurace;
    }


}
