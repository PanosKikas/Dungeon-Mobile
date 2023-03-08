using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public virtual void EnterState() { }
    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }
    public virtual void ExitState() { }

    protected Character Owner;
    
    public State(Character owner)
    {
        Owner = owner;
    }
}
