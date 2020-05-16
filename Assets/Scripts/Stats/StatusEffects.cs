using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;

public class StatsEvent : UnityEvent<CharacterStats> { }
public abstract class StatusEffects : MonoBehaviour
{

   /* public GameObject impactEffect;

    [SerializeField]
    Vector3 ImpactEffectOffset;*/

    bool hasDied = false;

    [HideInInspector]
    public static StatsEvent OnHpLoss;

  //  public abstract CharacterStats stats { get; }
    
    /*public virtual void TakeDamage(CharacterStats stats, int damage, GameObject impactEffect = null)
    {  
        stats.CurrentHealth = Mathf.Clamp(stats.CurrentHealth - damage, 0, stats.MaxHealth);
        
        OnHpLoss?.Invoke(stats);

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
    }  */

    public static void DamageTarget(CharacterStats stats, int damage)
    {
        stats.TakeDamage(damage);
    }

    public static bool Heal(CharacterStats stats, int health)
    {
        if (!stats.HasMaxHealth())
        {
            stats.CurrentHealth = (int)Mathf.MoveTowards(stats.CurrentHealth, stats.MaxHealth, health);
            return true;
        }
        return false;
    }

    /*protected virtual void Die()
    {
        Debug.Log("Die");
        
        enabled = false;
        gameObject.SetActive(false);
    }*/
}
