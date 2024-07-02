using DMT.Characters;
using DMT.Characters.Stats;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;
    
    private Animator animator;

    private IDamagable player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject.TryGetComponent<IDamagable>(out var damagable))
        {
            player = damagable;
            Activate();
        }
    }

    private void Activate()
    {
        animator.SetBool("Activated", true);
    }

    public void DamagePlayer()
    {
        if (player == null)
        {
            return;
        }
        player.TakeDamage(damage);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
            animator.SetBool("Activated", false);
        }
    }
}
