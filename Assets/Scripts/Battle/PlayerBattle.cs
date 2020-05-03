using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;


public class PlayerBattle : MonoBehaviour
{

    Animator animator;
    
    GameObject enemyTarget;

    [SerializeField]
    LayerMask enemyLayerMask;

    CinemachineImpulseSource impulseSource;
    StatusEffects effects;

    float nextFireTime = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        effects = GetComponent<StatusEffects>();
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && TimeToAttack())
        {
            FindAttackTarget();
            if (enemyTarget != null)
            {
                AttackTarget();
                nextFireTime = Time.time + 1f/effects.stats.ManualAttackRate;
                ShakeCamera();
            }
        }
    }

    bool TimeToAttack()
    {
        return Time.time >= nextFireTime;
    }

    void FindAttackTarget()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosition, 2f, enemyLayerMask);
        
        if (colliders != null && colliders.Any<Collider2D>())
        {
            enemyTarget = colliders[0].gameObject;
        }
        else
        {
            enemyTarget = null;
        }
    }

    void AttackTarget()
    {
        PlayAttackAnimation();

        DamageEnemyTarget();
        ShakeCamera();
    }

    void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    void DamageEnemyTarget()
    {
        IDamagable target = enemyTarget.GetComponent<IDamagable>();
        target.TakeDamage(effects.stats.BaseAttackDamage, effects.impactEffect);
    }

    void ShakeCamera()
    {
        impulseSource.GenerateImpulse();
    }
}
