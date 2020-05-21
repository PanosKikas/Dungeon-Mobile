using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//[System.Serializable] public class CharacterStatDictionary : SerializableDictionary<string, CharacterStat> { }
public abstract class CharacterStats : ScriptableObject
{
        
    public static List<CharacterStat> upgradableStatsList;

    public CharacterStat MaxHealthStat = new CharacterStat(Stat.MaxHealth);
    public CharacterStat AttackDamageStat = new CharacterStat(Stat.AttackDamage);
    public CharacterStat AutoAttackRateStat = new CharacterStat(Stat.AttackSpeed);

    private void OnEnable()
    {
        Initialize();
    }

    protected virtual void Initialize()
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
    
    
    public int AttackDamage
    {
        get
        {
            return (int)AttackDamageStat.Value;
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

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, (int)MaxHealth);

      

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

    public static CharacterStat FindCharacterStat(Stat type)
    {
        return upgradableStatsList.Find(i => (i.Type == type));
    }
}
