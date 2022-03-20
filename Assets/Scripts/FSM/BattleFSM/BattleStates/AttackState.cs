using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : State
{
    protected CharacterBattle battle;
    protected CharacterStats stats;

    protected float nextFire;

    public AttackState(BattleFSM stateMachine)
    {
        battle = stateMachine.GetComponent<CharacterBattle>();
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
