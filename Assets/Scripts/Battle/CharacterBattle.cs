using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBattle: MonoBehaviour 
{
    protected Animator animator;
    public CharacterStats stats;
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
