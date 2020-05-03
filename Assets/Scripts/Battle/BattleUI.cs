using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{   
    Slider hpBar;
    StatusEffects statusEffects;

    private void Awake()
    {
        hpBar = GetComponentInChildren<Slider>();
        statusEffects = GetComponentInParent<StatusEffects>();
        statusEffects.OnHpLoss.AddListener(UpdateHealth);
    }
    
    void Start()
    {
        UpdateHealth();       
    }

    
    public void UpdateHealth()
    {
        hpBar.value = (float)statusEffects.CurrentHealth / statusEffects.stats.MaxHealth;
    }
    
}
