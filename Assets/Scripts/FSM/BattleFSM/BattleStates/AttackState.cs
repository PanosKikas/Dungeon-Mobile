using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : State
{
    protected CharacterBattleController BattleController;
    protected CharacterStats stats;

    protected float nextFire;
    protected Character Target;

    public AttackState(Character owner) : base(owner)
    {
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
