using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    int damage = 10;

    Animator animator;

    PlayerCharacterStats playerStats;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerStats = StatsDatabase.Instance.GetMainCharacterStats();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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

    public void DamagePlayer()
    {
        StatusEffects.DamageTarget(playerStats, damage);     
    }

    
}
