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
    PlayerStatusEffects playerStatusEffects;

    float nextFireTime = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        playerStatusEffects = GetComponent<PlayerStatusEffects>();
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && CanAttack())
        {
            FindAttackTarget();
            if (enemyTarget != null)
            {
                AttackTarget();
                nextFireTime = Time.time + 1f/playerStatusEffects.stats.ManualAttackRate;
                ShakeCamera();
            }
        }
    }

    bool CanAttack()
    {
        return TimeToAttack() && HasEndurance();
    }

    bool TimeToAttack()
    {
        return Time.time >= nextFireTime;
    }

    bool HasEndurance()
    {
        return playerStatusEffects.CurrentEndurance >= playerStatusEffects.stats.EndurancePerAttack;
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
        playerStatusEffects.DecreaseEndurance();
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
        target.TakeDamage(playerStatusEffects.stats.BaseAttackDamage, playerStatusEffects.impactEffect);
    }

    void ShakeCamera()
    {
        impulseSource.GenerateImpulse();
    }
}
