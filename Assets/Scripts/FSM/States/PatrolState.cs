using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{ 

    [SerializeField]
    float searchRadius = 5f;

    Rigidbody2D rb;
    
    Transform[] patrolWaypoints;

    int currentWaypointIndex;
    Transform currentWaypoint;
    Vector3 targetWaypointPosition;

    [SerializeField]
    float speed = 5f;

    Vector2 velocity;

    Vector3 previousPosition;

    SpriteRenderer spriteRenderer;

    EnemyBehavior enemyBehavior;
    EnemyFSM stateMachine;

    public PatrolState(EnemyFSM stateMachine)
    {
        
        rb = stateMachine.GetComponent<Rigidbody2D>();
        spriteRenderer = stateMachine.GetComponentInChildren<SpriteRenderer>();
        enemyBehavior = stateMachine.GetComponent<EnemyBehavior>();
        this.stateMachine = stateMachine;
        var EnemyWaypoints = stateMachine.GetComponent<EnemyWaypoints>();
          
        patrolWaypoints = EnemyWaypoints.PatrolWaypoints;
       
    }

    public override void EnterState()
    {
        currentWaypointIndex = Random.Range(0, patrolWaypoints.Length);
        currentWaypoint = patrolWaypoints[currentWaypointIndex];
        targetWaypointPosition = currentWaypoint.position + Random.onUnitSphere * .4f;
    }


    public override void LogicUpdate()
    {        

        if (CloseToWaypoint())
        {
            ArriveAtWaypoint();
        }
     
    }

    public override void PhysicsUpdate()
    {

        var direction = GetMoveDirection();

        rb.MovePosition(stateMachine.transform.position + direction * speed * Time.fixedDeltaTime);
        UpdateVelocity();

        previousPosition = stateMachine.transform.position;
        enemyBehavior.Velocity = velocity;
    }




    Vector3 GetMoveDirection()
    {
        return (targetWaypointPosition - stateMachine.transform.position).normalized;
    }

    bool CloseToWaypoint()
    {
        return Vector2.Distance(stateMachine.transform.position, targetWaypointPosition) <= .1f;
    }

    void ArriveAtWaypoint()
    {
        stateMachine.transform.position = targetWaypointPosition;
        velocity = Vector3.zero;
        stateMachine.ChangeState(stateMachine.WaitState);
    }


    void UpdateVelocity()
    {
        velocity = (stateMachine.transform.position - previousPosition) / Time.deltaTime;
        velocity *= speed;
    }

    public override void ExitState()
    {
        enemyBehavior.Velocity = Vector2.zero;
    }
    

}
