using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : BattleUI
{
   
    protected override void Start()
    {
        base.Start();
        
        stats.OnHpLoss.AddListener(UpdateHealth);
        
    }    

    public void UpdateHealth()
    {
        bar.value = (float)stats.CurrentHealth / stats.MaxHealth;
    }
}
