using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyFSM : FSM
{
    public ChaseState ChaseState { get; private set; }
    public PatrolState PatrolState { get; private set; }
    public WaitState WaitState { get; private set; }
    
    public EnemyFSM(Character owner)
    {
        ChaseState = new ChaseState(owner);
        PatrolState = new PatrolState(this, owner);
        WaitState = new WaitState(this, owner);

        currentState = PatrolState;
           
        currentState.EnterState();
    }
}
