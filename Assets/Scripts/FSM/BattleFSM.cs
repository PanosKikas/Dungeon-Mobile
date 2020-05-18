using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleFSM : FSM
{
    public AutoAttackState AutoAttackState { get; protected set; }

    protected virtual void Start()
    {
        AutoAttackState = new AutoAttackState(this);
        currentState = AutoAttackState;
        currentState.EnterState();
    }

    
}
