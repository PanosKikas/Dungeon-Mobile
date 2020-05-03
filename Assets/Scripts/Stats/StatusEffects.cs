using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;

public class StatusEffects : MonoBehaviour, IDamagable
{
    public CharacterStats stats;
        
    public GameObject impactEffect;
    [SerializeField]
    Vector3 ImpactEffectOffset;

    public int CurrentHealth { get; private set; }
    public int CurrentEndurace { get; private set; }
    
    IMovementDebuffs movementDebuffs;

    bool hasDied = false;

    public UnityEvent OnHpLoss;

    void Awake()
    {
        movementDebuffs = (IMovementDebuffs)GetComponent(typeof(IMovementDebuffs));
        Initialize();
    }
    
    void Initialize()
    {
        CurrentHealth = stats.MaxHealth;
        CurrentEndurace = stats.MaxEndurace;
    }
    
    public void TakeDamage(int damage, GameObject impactEffect)
    {  
        movementDebuffs?.DebuffMovement();

        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, stats.MaxHealth);
        
      
        OnHpLoss?.Invoke();
   

        if (impactEffect != null)
        {
            Vector3 impactPosition = transform.position + ImpactEffectOffset + Random.onUnitSphere;
            GameObject impact = Instantiate(impactEffect, impactPosition, Quaternion.identity);
            Destroy(impact, 2f);
        }
        

        if (CurrentHealth <= 0 && !hasDied)
        {
            hasDied = true;
            Die();
        }
    }  

   

    public void Heal(int health)
    {
        CurrentHealth += health;
    }

    void Die()
    {
        Debug.Log("Die");
        Destroy(gameObject, .5f);
        enabled = false;
        gameObject.SetActive(false);
    }
}
