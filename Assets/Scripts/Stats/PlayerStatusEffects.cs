using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffects : StatusEffects
{
    public int CurrentEndurance { get; private set; }

    protected override void Initialize()
    {
        base.Initialize();
        CurrentEndurance = stats.MaxEndurace;
    }

    public void DecreaseEndurance()
    {
        CurrentEndurance = Mathf.Clamp(CurrentEndurance - stats.EndurancePerAttack, 0, stats.MaxEndurace);
    }

}
