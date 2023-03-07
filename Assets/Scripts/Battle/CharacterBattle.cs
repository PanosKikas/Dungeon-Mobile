using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBattle : MonoBehaviour 
{
    protected Animator animator;
    
    [HideInInspector]
    public CharacterStats stats;


    public CharacterBattle AutoAttackTarget;

    [HideInInspector]
    public CharacterBattle Target;
    protected Stat TargetStats;


    protected BattleFSM stateMachine;

/*    public bool IsBlocking
    {
        get
        {
            return stateMachine.currentState.Equals(stateMachine.ParryState);
        }
    }*/

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        stateMachine = GetComponent<BattleFSM>();
        
    }

    protected virtual void Update()
    {
        stateMachine.LogicUpdateCurrentState();
    }

    public virtual void AttackTarget()
    {
        PlayAttackAnimation();
        //if (!Target.IsBlocking)
       // DamageEnemyTarget();
    }

    void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public void DamageEnemyTarget()
    {
        StatusEffects.DamageTarget(Target.stats, stats.Data.AttackDamage);
    }

    public virtual bool HasAttackTarget()
    {
        return Target != null && !Target.stats.HasDied;
    }

    public void FindAutoAttackTarget()
    {
        Target = AutoAttackTarget;
    }

}
