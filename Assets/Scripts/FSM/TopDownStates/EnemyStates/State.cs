using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public virtual void EnterState() { }
    public virtual void LogicUpdate(float delta) { }

    public virtual void PhysicsUpdate(float delta) { }
    public virtual void HandleInput(Input input) {}
    public virtual void ExitState() { }
}
