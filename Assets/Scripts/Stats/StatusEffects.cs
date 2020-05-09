using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;

public abstract class StatusEffects : MonoBehaviour, IDamagable
{
    public CharacterStats stats;
        
    public GameObject impactEffect;

    [SerializeField]
    Vector3 ImpactEffectOffset;

    bool hasDied = false;

    [HideInInspector]
    public UnityEvent OnHpLoss;
    
    
    public virtual void TakeDamage(int damage, GameObject impactEffect = null)
    {  
        stats.CurrentHealth = Mathf.Clamp(stats.CurrentHealth - damage, 0, stats.MaxHealth);
        
        OnHpLoss?.Invoke();

        if (impactEffect != null)
        {
            Vector3 impactPosition = transform.position + ImpactEffectOffset + Random.onUnitSphere;
            GameObject impact = Instantiate(impactEffect, impactPosition, Quaternion.identity);
            Destroy(impact, 2f);
        }
        
        if (stats.CurrentHealth <= 0 && !hasDied)
        {
            hasDied = true;
            Die();
        }
    }  

    public void Heal(int health)
    {
        stats.CurrentHealth += health;
    }

    protected virtual void Die()
    {
        Debug.Log("Die");
        Destroy(gameObject, .5f);
        enabled = false;
        gameObject.SetActive(false);
    }
}
