using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyFSM : MonoBehaviour
{
    
    public ChaseState ChaseState { get; private set; }
    public PatrolState PatrolState { get; private set; }
    public WaitState WaitState { get; private set; }

    public State currentState { get; private set; }

    private void Awake()
    {
        
    }

    private void Start()
    {
        ChaseState = new ChaseState(this);
        PatrolState = new PatrolState(this);
        WaitState = new WaitState(this);

        currentState = PatrolState;
           
        currentState.EnterState();
    }

    public void ChangeState(State newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
       
    }
 



}
