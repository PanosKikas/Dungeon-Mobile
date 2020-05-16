using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : BattleUI
{
   

    protected override void Start()
    {
        base.Start();
       // var statusEffects = GetComponent<CharacterBattle>().statusEffects;
        StatusEffects.OnHpLoss.AddListener(UpdateHealth);
        
    }
    


    public void UpdateHealth(CharacterStats stats)
    {
        bar.value = (float)stats.CurrentHealth / stats.MaxHealth;
    }

}
