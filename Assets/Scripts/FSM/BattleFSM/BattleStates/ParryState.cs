using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryState : State
{
    float TimeOnParry = .5f;
    float timer;
    BattleFSM stateMachine;

    public ParryState(BattleFSM stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void EnterState()
    {
        Debug.Log(" Enter Parry");
        timer = 0f;
    }

    public override void LogicUpdate()
    {
        if (timer >= TimeOnParry)
        {
            stateMachine.ChangeToPreviousState();
        }
        timer += Time.deltaTime;
        
    }

    public override void ExitState()
    {
        Debug.Log(" Exit Parry");
    }
}
