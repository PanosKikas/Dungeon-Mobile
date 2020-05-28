using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleFSM : FSM
{

    private State previousState = null;

    public AutoAttackState AutoAttackState { get; protected set; }
    public ParryState ParryState { get; protected set; }



    protected virtual void Start()
    {
        AutoAttackState = new AutoAttackState(this);
        ParryState = new ParryState(this);

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
