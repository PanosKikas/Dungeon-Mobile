using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyFSM : FSM
{
    public ChaseState ChaseState { get; private set; }
    public PatrolState PatrolState { get; private set; }
    public WaitState WaitState { get; private set; }

    private void Start()
    {
        ChaseState = new ChaseState(this);
        PatrolState = new PatrolState(this);
        WaitState = new WaitState(this);

        currentState = PatrolState;
           
        currentState.EnterState();
    }

}
