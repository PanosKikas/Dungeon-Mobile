using DMT.Character;
using DMT.Character.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    int damage = 10;

    Animator animator;

    CharacterStats playerStats;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.TakeDamage(damage);
            animator.SetBool("Activated", true);
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Activated", false);
        }
        
    }
}
