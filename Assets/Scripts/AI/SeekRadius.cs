using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class SeekRadius : MonoBehaviour
{
    AIPath path;
    AIDestinationSetter destinationSetter;

    [SerializeField]
    LayerMask searchMask;

    [SerializeField]
    Animator animator;

    [SerializeField]
    float searchRadius = 6f;

    private void Awake()
    {
        path = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        StartCoroutine(Search());
    }
   
    IEnumerator Search()
    {

        while(true)
        {
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, searchRadius, searchMask);
            if (colliders != null && colliders.Length > 0)
            {
                EnablePathFinder();

            }
            else if (destinationSetter.enabled)
            {
                DisablePathfinder();
            }
            yield return new WaitForSeconds(.2f);
        }      
    }  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnterBattle();
            Destroy(gameObject);
        }
    }

    void EnterBattle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void EnablePathFinder()
    {   
        animator.SetTrigger("Walk");
        path.enabled = true;
        destinationSetter.enabled = true;
        
    }

    void DisablePathfinder()
    {
        animator.SetTrigger("Idle");
        
        path.enabled = false;
        destinationSetter.enabled = false;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }


}
