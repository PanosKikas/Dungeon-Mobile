using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("Activate");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetTrigger("DeActivate");
    }
}
