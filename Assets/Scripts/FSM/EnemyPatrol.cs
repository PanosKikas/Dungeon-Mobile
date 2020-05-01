using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    float patrolRadius = 5f;

    Rigidbody2D rb;
    Vector3 destination;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartPatrol();
    }


    private void FixedUpdate()
    {
        if (destination == null)
        {
            return;
        }

        
    }

    void StartPatrol()
    {
        PickNextWaypoint();
    }

    void PickNextWaypoint()
    {
        destination = Random.onUnitSphere * patrolRadius;
        //rb.MoveTowards(transform.position, )
    }
}
