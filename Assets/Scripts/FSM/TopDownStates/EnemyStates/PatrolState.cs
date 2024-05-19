using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrolState : State
{
    private PatrolData patrolData;
    private float speed => patrolData.Speed;
    private float searchRadius => patrolData.Radius;

    private FSM stateMachine;

    private Transform[] patrolPoints;

    int currentWaypointIndex;
    Transform currentWaypoint;
    Vector3 targetWaypointPosition;

    EnemyController controller;

    private float currentWaitTime = 0f;
    private float waitSeconds => patrolData.ArriveWaitTime;

    public PatrolState(EnemyController controller, FSM stateMachine,
                IEnumerable<Transform> wayPoints, PatrolData patrolData)
    {
        this.controller = controller;
        this.stateMachine = stateMachine;
        this.patrolData = patrolData;
        patrolPoints = wayPoints.ToArray();
    }

    public override void EnterState()
    {
        ChooseRandomWaypoint();
    }

    private void ChooseRandomWaypoint()
    {
        List<Transform> patrolPointsToChoose = new List<Transform>(patrolPoints);
        if (currentWaypoint != null)
        {
            patrolPointsToChoose.Remove(currentWaypoint);
        }
        
        currentWaypointIndex = Random.Range(0, patrolPointsToChoose.Count());
        currentWaypoint = patrolPointsToChoose[currentWaypointIndex];
        targetWaypointPosition = currentWaypoint.position + Random.onUnitSphere * .4f;
    }

    public override void LogicUpdate(float delta)
    {
        if (currentWaitTime > 0f)
        {
            currentWaitTime -= delta;
            return;
        }

        if (CloseToWaypoint())
        {
            ArriveAtWaypoint();
        }
        else
        {
            var direction = GetMoveDirection();
            controller.Velocity = direction * speed;
        }
    }

    Vector3 GetMoveDirection()
    {
        return (targetWaypointPosition - controller.transform.position).normalized;
    }

    bool CloseToWaypoint()
    {
        return Vector2.Distance(controller.transform.position, targetWaypointPosition) <= .1f;
    }

    void ArriveAtWaypoint()
    {
        //_source.position = targetWaypointPosition;
        controller.Velocity = Vector2.zero;
        currentWaitTime = waitSeconds;
        ChooseRandomWaypoint();
    }

    public override void ExitState()
    {
        controller.Velocity = Vector2.zero;
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Projectile"))
        {
            stateMachine.ChangeState(controller.chaseState);
        }
    }
}
