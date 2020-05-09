using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnduranceUI : BattleUI
{
        
    private void Update()
    {
        UpdateEnduranceBar();
    }

    void UpdateEnduranceBar()
    {
       
        bar.value = (float)stats.CurrentEndurance / (float)stats.MaxEndurace;
    }


}
