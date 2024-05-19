using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChaseState : State
{
    AIDestinationSetter destinationSetter;
    AIPath path;
    EnemyController controller;
    EnemyGroup parentGroup;
    private FSM stateMachine;

    public ChaseState(EnemyController controller, EnemyGroup enemyGroup, FSM stateMachine)
    {
        this.controller = controller;
        this.stateMachine = stateMachine;
        destinationSetter = controller.GetComponent<AIDestinationSetter>();
        path = controller.GetComponent<AIPath>();
        parentGroup = enemyGroup;
    }

    public override void EnterState()
    {
        // parentGroup.EnableChaseAllEnemies();
        EnablePathFinder();
    }

    public override void ExitState()
    {
        DisablePathfinder();
    }

    public override void LogicUpdate(float delta)
    {
        controller.Velocity = path.desiredVelocity;
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

    public override void OnTriggerExit(Collider2D collider)
    {
        base.OnTriggerExit(collider);
        if (collider.CompareTag("Player"))
        {
            stateMachine.ChangeState(controller.patrolState);
        }
    }
}
