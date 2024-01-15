using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    private State previousState = null;
    public State currentState { get; protected set; }

    public FSM(State initialState)
    {
        ChangeState(initialState);
    }
    
    public void ChangeState(State newState)
    {
        previousState = currentState;
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void LogicUpdate()
    {
        currentState.LogicUpdate();
    }
    public void PhysicsUpdate()
    {
        currentState.PhysicsUpdate();
    }
    
    public void ChangeToPreviousState()
    {
        ChangeState(previousState);
    }
}
