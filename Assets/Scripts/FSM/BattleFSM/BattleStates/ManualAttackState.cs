using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using DMT.Character;

public class ManualAttackState : State
{
    private readonly Character source;
    private Character target;
    
    public ManualAttackState(Character source)
    {
        this.source = source;
    }

    private bool CanAttack()
    {
        return source.stats.CurrentEndurance > 0;
    }

    public override void LogicUpdate(float delta)
    {
        base.LogicUpdate(delta);
        
        if (CanAttack() && Input.GetButtonDown("Fire1"))
        {
            target = FindClosestTargetToMousePosition();
        }

        if (target == null)
        {
            return;
        }
        
        AttackTarget();
    }
    
    private Character FindClosestTargetToMousePosition()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosition, 2f,
            LayerMask.GetMask("Enemy"));
        if (colliders != null && colliders.Any())
        {
            return colliders[0].gameObject.GetComponent<Character>();
        }

        return null;
    }

    private void AttackTarget()
    {
        source.stats.CurrentEndurance -= 50;
        target.TakeDamage(source.stats.AttackDamage);
        target = null;
    }
}
