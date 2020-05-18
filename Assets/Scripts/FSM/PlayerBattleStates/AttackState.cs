using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState<T, U> : State where T: CharacterStats where U: CharacterBattle<T>
{
    protected T stats;
    protected U battle;

    protected float nextFire;

    public AttackState(FSM stateMachine)
    {
        battle = stateMachine.GetComponent<U>();
        Debug.Log(battle);
        stats = battle.stats;
    }

    public override void EnterState()
    {
        base.EnterState();
        nextFire = 0;
    }

    protected virtual bool CanAttack()
    {
        return Time.time >= nextFire;
    }

    protected abstract void FindTarget();
    

}
