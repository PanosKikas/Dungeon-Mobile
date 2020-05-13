using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnduranceUI : BattleUI
{
    private PlayerCharacterStats stats;

    protected override void Awake()
    {
        base.Awake();
        stats = (PlayerCharacterStats)statusEffects.stats;
    }

    private void Update()
    {
        UpdateEnduranceBar();
    }

    void UpdateEnduranceBar()
    {
       
        bar.value = (float)stats.CurrentEndurance / (float)stats.MaxEndurace;
    }


}
