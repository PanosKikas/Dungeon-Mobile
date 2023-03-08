using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChaseState : State
{
    AIDestinationSetter destinationSetter;
    AIPath path;
    EnemyController enemyController;
    EnemyGroup parentGroup;

    public ChaseState(EnemyController owner) : base(owner.Character)
    {
        destinationSetter = owner.GetComponent<AIDestinationSetter>();
        path = owner.GetComponent<AIPath>();
        
        enemyController = owner.GetComponent<EnemyController>();
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
        enemyController.Velocity = path.desiredVelocity;
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
