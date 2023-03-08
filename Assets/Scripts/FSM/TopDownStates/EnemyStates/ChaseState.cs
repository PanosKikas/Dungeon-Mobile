using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChaseState : State
{
    AIDestinationSetter destinationSetter;
    AIPath path;
    EnemyBehavior enemyBehavior;
    EnemyGroup parentGroup;

    public ChaseState(Character owner) : base(owner)
    {
        destinationSetter = owner.GetComponent<AIDestinationSetter>();
        path = owner.GetComponent<AIPath>();
        
        enemyBehavior = owner.GetComponent<EnemyBehavior>();
        parentGroup = owner.GetComponentInParent<EnemyGroup>();
    }

    public override void EnterState()
    {
        parentGroup.EnableChaseAllEnemies();
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
