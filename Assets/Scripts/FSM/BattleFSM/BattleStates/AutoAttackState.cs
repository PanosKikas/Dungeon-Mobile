using System.Collections;
using System.Collections.Generic;
using DMT.Character;
using UnityEngine;

public class AutoAttackState : State
{
    private float nextAttackCountdown;
    private readonly Character _source;
    private readonly Character _target;
    
    public AutoAttackState(Character source, Character target)
    {
        nextAttackCountdown = source.stats.AutoAttackRateStat.Value;
        _source = source;
        _target = target;
    }

    public override void EnterState()
    {
        base.EnterState();
        nextAttackCountdown = 0;
    }

    private bool CanAttack()
    {
        return nextAttackCountdown <= 0;
    }

    public override void LogicUpdate(float delta)
    {
        base.LogicUpdate(delta);
        nextAttackCountdown -= delta;
        if (CanAttack())
        {
            nextAttackCountdown = _source.stats.AutoAttackRateStat.Value;
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        _target.TakeDamage(_source.stats.AttackDamage);
    }
}
