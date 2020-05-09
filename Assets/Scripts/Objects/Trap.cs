using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    int damage = 10;

    Animator animator;
    PlayerStatusEffects statusEffects;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Activated", true);
            statusEffects = collision.gameObject.GetComponent<PlayerStatusEffects>();
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Activated", false);
            statusEffects = null;
        }
        
    }

    public void DamagePlayer()
    {
        statusEffects?.TakeDamage(damage);
        
    }

    
}
