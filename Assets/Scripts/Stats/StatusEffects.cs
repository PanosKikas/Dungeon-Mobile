using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class StatusEffects : MonoBehaviour, IDamagable
{

    public CharacterStats stats;

    [SerializeField]
    int CurrentHealth;

    IMovementDebuffs movementDebuffs;

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
    
    public void TakeDamage(int damage)
    {
        
        movementDebuffs?.DebuffMovement();

        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }  

    public void Heal(int health)
    {
        CurrentHealth += health;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
