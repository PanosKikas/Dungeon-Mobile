using System;
using System.Collections;
using System.Collections.Generic;
using EventArgs;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterStatsSO _initialStats;

    private Animator animator;
    
    public CharacterStats Stats { get; private set; }

    public event Action<CharacterDamagedArgs> CharacterDamaged;
    
    public int Health { get; private set; }
    public int Stamina { get; private set; }

    public bool IsAlive => Health > 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Stats = new CharacterStats(_initialStats);
        Health = (int)Stats.MaxHealth.Value;
        Stamina = (int)Stats.MaxStamina.Value;
    }

    public void Attack(Character target)
    {
        animator.SetTrigger("Attack");
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
