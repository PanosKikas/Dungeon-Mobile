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

   

    private void Awake()
    {
        animator = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        effects = GetComponent<StatusEffects>();
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FindAttackTarget();
            if (enemyTarget != null)
            {
                AttackTarget();
                ShakeCamera();
            }
        }
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
        animator.SetTrigger("Attack");
        enemyTarget.GetComponent<IDamagable>().TakeDamage(effects.stats.MainAttackDamage, effects.impactEffect);

        ShakeCamera();
    }

    void ShakeCamera()
    {
        impulseSource.GenerateImpulse();
    }
}
