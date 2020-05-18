using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBattle<T>: MonoBehaviour  where T: CharacterStats
{
    protected Animator animator;
    public T stats;
    public CharacterStats AutoAttackTarget;
    public CharacterStats Target;
    FSM stateMachine;

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        stateMachine = GetComponent<FSM>();
    }

    protected virtual void Update()
    {
        stateMachine.LogicUpdateCurrentState();
    }

    public virtual void AttackTarget()
    {
        PlayAttackAnimation();
        DamageEnemyTarget();
    }

    void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    void DamageEnemyTarget()
    {
        StatusEffects.DamageTarget(Target, stats.AttackDamage);
    }

    public virtual bool HasAttackTarget()
    {
        return Target != null && !Target.HasDied;
    }

    public void FindAutoAttackTarget()
    {
        Target = AutoAttackTarget;
    }

}
