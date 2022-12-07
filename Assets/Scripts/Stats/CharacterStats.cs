using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public CharacterStatsSO InitialData;

    public int CurrentHealth;
    
    public CharacterStats(CharacterStatsSO initialData)
    {
        InitialData = initialData;
        InitialData.Initialize();
        CurrentHealth = initialData.MaxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHealth = (int)Mathf.Clamp(CurrentHealth - damage, 0, Int32.MaxValue);
;    }
}
