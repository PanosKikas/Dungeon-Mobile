using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnduranceUI : BattleUI
{
    private PlayerCharacterStats stats;

    protected override void Start()
    {
        base.Start();
        

    }
    
    public void UpdateEnduranceBar(PlayerCharacterStats stats)
    {
       
        bar.value = (float)stats.CurrentEndurance / (float)stats.MaxEndurace;
    }


}
