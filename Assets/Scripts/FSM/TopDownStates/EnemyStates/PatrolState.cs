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

    EnemyController enemyController;
    EnemyFSM stateMachine;

    public PatrolState(EnemyFSM stateMachine, EnemyController owner) : base(owner.Character)
    {
        
        rb = owner.GetComponent<Rigidbody2D>();
        enemyController = owner;
        spriteRenderer = enemyController.GetComponentInChildren<SpriteRenderer>();
        var enemyWaypoints = enemyController.GetComponentInParent<EnemyWaypoints>();
        this.stateMachine = stateMachine;
          
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

        var position = enemyController.transform.position;
        rb.MovePosition(position + direction * (speed * Time.fixedDeltaTime));
        UpdateVelocity();

        previousPosition = position;
        enemyController.Velocity = velocity;
    }

    Vector3 GetMoveDirection()
    {
        return (targetWaypointPosition - enemyController.transform.position).normalized;
    }

    bool CloseToWaypoint()
    {
        return Vector2.Distance(enemyController.transform.position, targetWaypointPosition) <= .1f;
    }

    void ArriveAtWaypoint()
    {
        enemyController.transform.position = targetWaypointPosition;
        velocity = Vector3.zero;
        stateMachine.ChangeState(stateMachine.WaitState);
    }

    void UpdateVelocity()
    {
        velocity = (enemyController.transform.position - previousPosition) / Time.deltaTime;
        velocity *= speed;
    }

    public override void ExitState()
    {
        enemyController.Velocity = Vector2.zero;
    }
    

}
