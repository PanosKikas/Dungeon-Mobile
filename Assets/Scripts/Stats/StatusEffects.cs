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
    
    public void TakeDamage(int damage)
    {
        
        movementDebuffs?.DebuffMovement();

        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, stats.MaxHealth);
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
