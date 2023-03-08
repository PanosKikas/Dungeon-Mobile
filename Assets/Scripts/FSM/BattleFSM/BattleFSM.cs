using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFSM : FSM
{
    private State previousState = null;
    public AutoAttackState AutoAttackState { get; protected set; }
    public ManualAttackState ManualAttackState { get; private set; }
    
    public BattleFSM(Character owner)
    {
        AutoAttackState = new AutoAttackState(owner);
        ManualAttackState = new ManualAttackState(owner);
        currentState = AutoAttackState;

        currentState.EnterState();
    }

    public override void ChangeState(State newState)
    {
        previousState = currentState;
        base.ChangeState(newState);
    }

    public void ChangeToPreviousState()
    {
        ChangeState(previousState);
    }
}
