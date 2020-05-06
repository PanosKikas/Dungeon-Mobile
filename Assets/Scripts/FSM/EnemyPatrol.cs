using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class EnemyPatrol : MonoBehaviour
{
   
    float waitTime;
    [SerializeField]
    float maxWaitTime;

    [SerializeField]
    float searchRadius = 5f;

    Rigidbody2D rb;

    [SerializeField]
    Transform[] patrolWaypoints;

    int currentWaypointIndex;
    Transform currentWaypoint;

    [SerializeField]
    float speed = 5f;

    float currentSpeed;

    Animator animator;
    AIPath aiPath;

    Vector2 velocity;

    Vector3 previousPosition;

    SpriteRenderer spriteRenderer;
    AIDestinationSetter destinationSetter;
    AIPath path;
    

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        currentWaypointIndex = Random.Range(0, patrolWaypoints.Length);
        currentWaypoint = patrolWaypoints[currentWaypointIndex];
        aiPath = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        path = GetComponent<AIPath>();
    }


    private void Start()
    {
        waitTime = Random.Range(.5f, maxWaitTime);
        //StartCoroutine(Search());
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Projectile"))
        {
            EnablePathFinder();
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            DisablePathfinder();
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
        path.enabled = true;
        destinationSetter.enabled = true;
        
    }

    void DisablePathfinder()
    {

        path.enabled = false;
        destinationSetter.enabled = false;

    }

    private void Update()
    {
        if (path.enabled)
        {
            animator.SetFloat("Speed", path.maxSpeed);
            if (path.desiredVelocity.x > 0.1f)
            {
                spriteRenderer.flipX = true;
                
            }
            else if (path.desiredVelocity.x < 0.1f && spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (path.enabled)
            return;

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
            Debug.Log(spriteRenderer.flipX);
        }
        else if (velocity.x < 0.1f && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }

        velocity *= speed;

    
        animator.SetFloat("Speed", velocity.magnitude);
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
    }
}
