using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    GameObject target;

    IDamagable currentTarget;

    StatusEffects statusEffects;

    CharacterStats stats;

    float rateOfFire = .5f;
    float nextFire = 0;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        statusEffects = GetComponent<StatusEffects>();
        currentTarget = target.GetComponent<IDamagable>();
        stats = statusEffects.stats;
    }

    private void Update()
    {
        if (target == null || !target.activeSelf)
            return;

        if (TimeToAttack())
        {
            
            nextFire = Time.time + 1 / rateOfFire;
            StartCoroutine(Attack());
        }
    }

    bool TimeToAttack()
    {
        return Time.time >= nextFire;
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length +
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime - 1f);

        DamageTarget();
    }

    void DamageTarget()
    {
        currentTarget.TakeDamage(stats.MainAttackDamage, statusEffects.impactEffect);
    }

}
