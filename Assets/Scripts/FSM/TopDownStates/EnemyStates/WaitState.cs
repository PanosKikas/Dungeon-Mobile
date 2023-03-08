using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : State
{

    float waitTime;

    float minWaitTime = .5f;

    float maxWaitTime = 3f;

    EnemyFSM stateMachine;

    public WaitState(EnemyFSM stateMachine, Character owner) : base(owner)
    {
        this.stateMachine = stateMachine;
    }

    public override void EnterState()
    {
        waitTime = Random.Range(minWaitTime, maxWaitTime); 
    }

    public override void LogicUpdate()
    {
        
        if (waitTime <= 0)
        {
            stateMachine.ChangeState(stateMachine.PatrolState);

        }
        waitTime -= Time.deltaTime;
    }
}
