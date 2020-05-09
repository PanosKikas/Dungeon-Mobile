using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : BattleUI
{
    
    
    void Start()
    {
        statusEffects.OnHpLoss.AddListener(UpdateHealth);
        UpdateHealth();
    }


    public void UpdateHealth()
    {
        bar.value = (float)stats.CurrentHealth / stats.MaxHealth;
    }

}
