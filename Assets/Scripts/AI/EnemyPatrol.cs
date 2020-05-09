using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class EnemyPatrol : MonoBehaviour
{
   
   


/*
    private void Start()
    {
        waitTime = Random.Range(.5f, maxWaitTime);
        
    }

    
    
    private void FixedUpdate()
    {

        var direction = (currentWaypoint.position - transform.position).normalized;
       
        rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position, currentWaypoint.position) <= .1f)
        {
            transform.position = currentWaypoint.position;
            if (waitTime <= 0)
            {
                waitTime = Random.Range(.5f, maxWaitTime);
                SwitchWaypoint();
            }
            else
            {
                currentSpeed = 0;
                waitTime -= Time.fixedDeltaTime;
            }
        }

        velocity = (transform.position - previousPosition) / Time.deltaTime;
        if(velocity.x > 0.1f)
        {
            spriteRenderer.flipX = true;
        }
        else if (velocity.x < 0.1f && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }

        velocity *= speed;


        movementAnimation.AnimateMovement(velocity);
        previousPosition = transform.position;


    }

    void SwitchWaypoint()
    {
        currentWaypointIndex = Random.Range(0, patrolWaypoints.Length);
        currentWaypoint = patrolWaypoints[currentWaypointIndex];
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }*/
}
