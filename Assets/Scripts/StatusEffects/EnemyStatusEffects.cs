using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusEffects : StatusEffects
{
   // IMovementDebuffs movementDebuffs;

    

    private void Awake()
    {
      //  movementDebuffs = (IMovementDebuffs)GetComponent(typeof(IMovementDebuffs));
    }
    

   /* public override void TakeDamage(CharacterStats stats, int damage, GameObject impactEffect)
    {
        base.TakeDamage(stats, damage, impactEffect);
        movementDebuffs?.DebuffMovement();
    }*/
}
