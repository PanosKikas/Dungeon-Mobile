using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackState : AttackState
{
    public AutoAttackState(Character owner) :base(owner)
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
            nextFire = Time.time + (1f / Owner.Stats.AutoAttackRate.Value);
            Owner.Attack(Target);
        }
    }

    protected override void FindTarget()
    {
        //BattleController.FindAutoAttackTarget();
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
