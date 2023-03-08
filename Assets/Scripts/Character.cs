using System;
using System.Collections;
using System.Collections.Generic;
using EventArgs;
using UnityEngine;

public class Character 
{
    public CharacterStats Stats { get; private set; }
    public CharacterEquipment Equipment { get; private set; }
    
    public event Action<CharacterDamagedArgs> CharacterDamaged;
    
    public int Health { get; private set; }
    public int Stamina { get; private set; }

    public bool IsAlive => Health > 0;

    // Start is called before the first frame update
    public Character(CharacterStatsSO initialStats)
    {
        Stats = new CharacterStats(initialStats);
        Health = (int)Stats.MaxHealth.Value;
        Stamina = (int)Stats.MaxStamina.Value;
        Equipment = new CharacterEquipment();
    }

    public void Attack(Character target)
    {
        target.TakeDamage(10);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
        CharacterDamaged?.Invoke(new CharacterDamagedArgs(Health, (int)Stats.MaxHealth.Value));
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
