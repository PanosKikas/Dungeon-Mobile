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

    public void LogicUpdate(float delta)
    {
        currentState.LogicUpdate(delta);
    }
    public void PhysicsUpdate(float delta)
    {
        currentState.PhysicsUpdate(delta);
    }
    
    public void ChangeToPreviousState()
    {
        ChangeState(previousState);
    }
}
