using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public abstract class CharacterStats : ScriptableObject
{
        
    protected List<CharacterStat> upgradableStatsList;

 

    public CharacterStat MaxHealthStat;
    public CharacterStat AttackDamageStat;
    public CharacterStat AutoAttackRateStat;
    

    public virtual void Initialize()
    {
        
        upgradableStatsList = new List<CharacterStat>
        {
           MaxHealthStat, AttackDamageStat, AutoAttackRateStat
            
        };

         
        CurrentHealth = (int)MaxHealthStat.Value;
        HasDied = false;
           
    }

    private int currentHealth;

    public int CurrentHealth
    {
        get
        {
            return (int)currentHealth;
        }
        set
        {
            currentHealth = value;
            OnHpLoss?.Invoke();
        }
    }

    public int MaxHealth
    {
        get
        {
            return (int)MaxHealthStat.Value;
        }
        
    }
    
    
    public virtual float AttackDamage
    {
        get
        {
            return AttackDamageStat.Value;
        }
       
    }

    public float AutoAttackRate
    {
        get
        {
            return AutoAttackRateStat.Value;
        }
        
    }

    public bool HasDied = false;

    public GameObject impactEffect;

    [SerializeField]
    Vector3 ImpactEffectOffset;
    public UnityEvent OnHpLoss;
    
    public bool HasMaxHealth()
    {
        return CurrentHealth == MaxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp((int)(CurrentHealth - damage), 0, MaxHealth);

      
        
        if (impactEffect != null)
        {
            //Vector3 impactPosition = transform.position + ImpactEffectOffset + Random.onUnitSphere;
           // GameObject impact = Instantiate(impactEffect, impactPosition, Quaternion.identity);
          //  Destroy(impact, 2f);
        }

        if (CurrentHealth <= 0 && !HasDied)
        {
            HasDied = true;
            Die();
        }
    }



    protected virtual void Die()
    {
        Debug.Log("Die");

        //this.enabled = false;
       // gameObject.SetActive(false);
    }

    public List<CharacterStat> FindCharacterStats(Stat type)
    {
        return upgradableStatsList.FindAll(i => (i.Type == type));
    }
}
