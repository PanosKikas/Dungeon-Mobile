using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackState : AttackState
{
    public AutoAttackState(BattleFSM stateMachine) :base(stateMachine)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        FindTarget();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (CanAttack())
        {
            nextFire = Time.time + (1f / stats.AutoAttackRate);
            battle.AttackTarget();
        }
    }

    protected override void FindTarget()
    {
        battle.FindAutoAttackTarget();
    }
}

/* IEnumerator Attack()
 {
     animator.SetTrigger("Attack");

     yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length +
         animator.GetCurrentAnimatorStateInfo(0).normalizedTime - 1f);

     StatusEffects.DamageTarget(target, stats.AttackDamage);
 }

*/
