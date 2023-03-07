using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnduranceUI : BattleUI
{
    protected override void Start()
    {
        base.Start();

    }

    public void UpdateEnduranceBar(int endurance, int maxEndurance)
    {
        bar.value = (float)endurance / maxEndurance;
    }
}
