using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnduranceUI : BattleUI
{
    protected override void Start()
    {
        base.Start();

    }
    private void Update()
    {
        //UpdateEnduranceBar();
    }
    
    public void UpdateEnduranceBar()
    {
        //bar.value = (float)character.playerStats.Endurance / (float)character.stats.MaxEndurace;
    }
}
