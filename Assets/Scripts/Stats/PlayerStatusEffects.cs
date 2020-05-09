using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffects : StatusEffects
{
    
    public void DecreaseEndurance()
    {
        stats.CurrentEndurance = Mathf.Clamp(stats.CurrentEndurance - stats.EndurancePerAttack, 0, stats.MaxEndurace);
    }

    private void Update()
    {
        RechargeEndurance(Time.deltaTime * stats.EnduranceRechargeRate);
    }

    void RechargeEndurance(float endurance)
    {
        stats.CurrentEndurance = Mathf.Clamp(stats.CurrentEndurance + endurance, 0f, stats.MaxEndurace);
    }

}
