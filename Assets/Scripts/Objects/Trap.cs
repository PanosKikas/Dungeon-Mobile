using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    int damage = 10;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var character = collision.GetComponent<Character>();
            character.TakeDamage(damage);
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
