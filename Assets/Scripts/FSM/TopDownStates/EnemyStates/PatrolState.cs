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

    public PatrolState(EnemyFSM stateMachine, Character owner) : base(owner)
    {
        
        rb = Owner.GetComponent<Rigidbody2D>();
        spriteRenderer = Owner.GetComponentInChildren<SpriteRenderer>();
        enemyBehavior = Owner.GetComponent<EnemyBehavior>();
        this.stateMachine = stateMachine;
        var enemyWaypoints = Owner.GetComponentInParent<EnemyWaypoints>();
          
        patrolWaypoints = enemyWaypoints.PatrolWaypoints;
       
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

        var position = Owner.transform.position;
        rb.MovePosition(position + direction * (speed * Time.fixedDeltaTime));
        UpdateVelocity();

        previousPosition = position;
        enemyBehavior.Velocity = velocity;
    }

    Vector3 GetMoveDirection()
    {
        return (targetWaypointPosition - Owner.transform.position).normalized;
    }

    bool CloseToWaypoint()
    {
        return Vector2.Distance(Owner.transform.position, targetWaypointPosition) <= .1f;
    }

    void ArriveAtWaypoint()
    {
        Owner.transform.position = targetWaypointPosition;
        velocity = Vector3.zero;
        stateMachine.ChangeState(stateMachine.WaitState);
    }


    void UpdateVelocity()
    {
        velocity = (Owner.transform.position - previousPosition) / Time.deltaTime;
        velocity *= speed;
    }

    public override void ExitState()
    {
        enemyBehavior.Velocity = Vector2.zero;
    }
    

}
