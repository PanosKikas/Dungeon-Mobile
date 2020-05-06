using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusEffects : StatusEffects
{
    IMovementDebuffs movementDebuffs;
   
    protected override void Initialize()
    {
        base.Initialize();
        movementDebuffs = (IMovementDebuffs)GetComponent(typeof(IMovementDebuffs));
    }

    public override void TakeDamage(int damage, GameObject impactEffect)
    {
        base.TakeDamage(damage, impactEffect);
        movementDebuffs?.DebuffMovement();
    }
}
