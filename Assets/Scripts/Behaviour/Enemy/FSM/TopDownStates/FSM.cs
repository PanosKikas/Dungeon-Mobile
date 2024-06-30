﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    private State previousState = null;
    public State currentState { get; protected set; }

    public FSM()
    {

    }

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
        currentState?.LogicUpdate(delta);
    }
    public void PhysicsUpdate(float delta)
    {
        currentState?.PhysicsUpdate(delta);
    }
    
    public void ChangeToPreviousState()
    {
        ChangeState(previousState);
    }

    public void OnTriggerEnter(Collider2D collider)
    {
        currentState?.OnTriggerEnter(collider);
    }

    public void OnTriggerExit(Collider2D collider)
    {
        currentState?.OnTriggerExit(collider);
    }
}