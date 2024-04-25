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

    public ChaseState(GameObject enemy)
    {
        destinationSetter = enemy.GetComponent<AIDestinationSetter>();
        path = enemy.GetComponent<AIPath>();
        
        enemyBehavior = enemy.GetComponent<EnemyBehavior>();
        parentGroup = enemy.GetComponentInParent<EnemyGroup>();
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
