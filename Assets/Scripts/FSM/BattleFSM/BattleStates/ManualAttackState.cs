using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;

public class ManualAttackState : AttackState
{
    CinemachineImpulseSource impulseSource;
    Character owner;

    public ManualAttackState(Character character) : base(character)
    {
        //impulseSource = stateMachine.GetComponent<CinemachineImpulseSource>();
        owner = character;
    }

    public override void EnterState()
    {
        base.EnterState();
       
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Input.GetButtonDown("Fire1") && CanAttack())
        {
            FindTarget();
            if (Target != null && Target.IsAlive)
            {
                Owner.Attack(Target);
                nextFire = Time.time + 1f / owner.Stats.AutoAttackRate.Value;
                //ShakeCamera();
            }
        }
    }

    protected override void FindTarget()
    {
        FindClosestTargetToMousePosition();
    }

    protected override bool CanAttack()
    {
        return base.CanAttack() && owner.Stamina > 0;
    }
    
    void FindClosestTargetToMousePosition()
    {
        if (Camera.main != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosition, 2f, LayerMask.NameToLayer("Enemy"));
            if (colliders != null && colliders.Any())
            {
                Target = colliders[0].gameObject.GetComponent<Character>();
            }
            else
            {
                Target = null;
            }
        }
    }

    /*void ShakeCamera()
    {
        impulseSource.GenerateImpulse();
    }*/
}
