using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class StatusEffects : MonoBehaviour, IDamagable
{
    public CharacterStats stats;
        
    public GameObject impactEffect;
    [SerializeField]
    Vector3 ImpactEffectOffset;

    [SerializeField]
    int CurrentHealth;

    IMovementDebuffs movementDebuffs;

    bool hasDied = false;

    void Awake()
    {
        movementDebuffs = (IMovementDebuffs)GetComponent(typeof(IMovementDebuffs));
        
    }

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        CurrentHealth = stats.MaxHealth;
    }
    
    public void TakeDamage(int damage, GameObject impactEffect)
    {  
        movementDebuffs?.DebuffMovement();

        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, stats.MaxHealth);

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
