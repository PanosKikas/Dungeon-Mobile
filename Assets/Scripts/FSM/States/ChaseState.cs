using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChaseState : State
{
    AIDestinationSetter destinationSetter;
    AIPath path;
    EnemyBehavior enemyBehavior;

    public ChaseState(EnemyFSM stateMachine)
    {
        
        destinationSetter = stateMachine.GetComponent<AIDestinationSetter>();
        path = stateMachine.GetComponent<AIPath>();
        
        enemyBehavior = stateMachine.GetComponent<EnemyBehavior>();
       
    }

    public override void EnterState()
    {
        
        EnablePathFinder();
        
    }

    public override void ExitState()
    {
        DisablePathfinder();
    }

    public override void LogicUpdate()
    {
        enemyBehavior.Velocity = path.desiredVelocity;

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

    

}
