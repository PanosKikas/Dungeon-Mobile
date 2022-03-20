using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSM : MonoBehaviour
{
    public State currentState { get; protected set; }

    public virtual void ChangeState(State newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void LogicUpdateCurrentState()
    {
        currentState.LogicUpdate();
    }
    public void PhysicsUpdateCurrentState()
    {
        currentState.PhysicsUpdate();
    }

}
