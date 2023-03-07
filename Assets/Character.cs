using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterStatsSO _initialStats;
    
    public CharacterStats Stats { get; private set; }
    public int Health { get; private set; }
    public int Stamina { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        Stats = new CharacterStats(_initialStats);
        Health = (int)Stats.MaxHealth.Value;
        Stamina = (int)Stats.MaxStamina.Value;
    }

    public void Attack()
    {
        
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    public void Heal(int health)
    {
        Health += health;
    }

    private void Die()
    {
        Debug.Log("You died");
    }
}
