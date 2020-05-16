using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;


public class PlayerBattle : CharacterBattle
{
    static int index = 0;
    
    
    CharacterStats enemyTarget;

    [SerializeField]
    LayerMask enemyLayerMask;

    CinemachineImpulseSource impulseSource;

    float nextFireTime = 0;

    PlayerCharacterStats playerStats;

    protected override void Start()
    {
        base.Start();
        impulseSource = GetComponent<CinemachineImpulseSource>();

        playerStats = (PlayerCharacterStats)stats;
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && CanAttack())
        {
            FindAttackTarget();
            if (enemyTarget != null)
            {
                AttackTarget();
                nextFireTime = Time.time + 1f/playerStats.ManualAttackRate;
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
        return playerStats.CurrentEndurance >= playerStats.EndurancePerAttack;
    }

    void FindAttackTarget()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosition, 2f, enemyLayerMask);
        
        if (colliders != null && colliders.Any<Collider2D>())
        {
            enemyTarget = colliders[0].gameObject.GetComponent<CharacterBattle>().stats;
            Debug.Log(enemyTarget);
        }
        else
        {
            enemyTarget = null;
        }
    }

    void AttackTarget()
    {
        PlayAttackAnimation();
        PlayerStatusEffects.DecreaseEndurance(playerStats);
        DamageEnemyTarget();
        ShakeCamera();
    }

    void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    void DamageEnemyTarget()
    {
        Debug.Log("Damage");
        /*StatusEffects target = enemyTarget.GetComponent<StatusEffects>(); 
        target.TakeDamage(stats.AttackDamage, statusEffects.impactEffect);*/
        StatusEffects.DamageTarget(enemyTarget, stats.AttackDamage);
    }

    void ShakeCamera()
    {
        impulseSource.GenerateImpulse();
    }
}
