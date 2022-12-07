using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour
{
    protected CharacterStats characterStats;
    public CharacterStatsSO Data;

    [HideInInspector]
    public UnityEvent OnHpChanged;
    
    public int Health
    {
        get => characterStats.CurrentHealth;
        set
        {
            characterStats.CurrentHealth = value;
            OnHpChanged?.Invoke();
        }
    }

    private void Start()
    {
        Initialize();
    }

    public bool HasDied => Health >= 0;
    
    protected virtual void Initialize()
    {
    }

    public virtual void TakeDamage(float damage)
    {
        
        Health = Mathf.Clamp((int)(Health - damage), 0, Data.MaxHealth);


        if (Data.impactEffect != null)
        {
            //Vector3 impactPosition = transform.position + ImpactEffectOffset + Random.onUnitSphere;
            // GameObject impact = Instantiate(impactEffect, impactPosition, Quaternion.identity);
            //  Destroy(impact, 2f);
        }

        if (HasDied)
        {
            Die();
        }
    }


    protected virtual void Die()
    {
        Debug.Log("Lol you died "  + gameObject);
        //this.enabled = false;
        // gameObject.SetActive(false);
    }

    public bool HasMaxHealth()
    {
        return characterStats.CurrentHealth == Data.MaxHealth;
    }

    protected abstract CharacterStats CreateStats();
}
