using System.Collections;
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
    
   
    private void Update()
    {
        animator.SetFloat("Speed", path.velocity.magnitude);
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }


}
