using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnduranceUI : BattleUI
{

    PlayerStatusEffects playerStatusEffects;

    protected override void Awake()
    {
        base.Awake();
        playerStatusEffects = (PlayerStatusEffects)statusEffects;
    }

    private void Update()
    {
        UpdateEnduranceBar();
    }

    void UpdateEnduranceBar()
    {
       
        bar.value = (float)playerStatusEffects.CurrentEndurance / (float)playerStatusEffects.stats.MaxEndurace;
    }


}
