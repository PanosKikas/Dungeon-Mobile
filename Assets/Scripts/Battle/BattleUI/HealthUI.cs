using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : BattleUI
{
    CharacterStats stats;

    protected override void Awake()
    {
        base.Awake();

        /*   if (GetComponent<StatusEffects>() == null)
           {
               Debug.Log(transform.parent.parent.name);
           }*/
        stats = statusEffects.stats;
    }

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
