using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    CharacterStats target;


//    CharacterBattle characterBattle;

    public CharacterStats stats;

    
    float nextFire = 0;


    private void Awake()
    {
        //characterBattle = GetComponent<CharacterBattle>();
        animator = GetComponentInChildren<Animator>();
        
        
        
       
    }

    private void Start()
    {
        //stats = characterBattle.;
    }

    private void Update()
    {
        if (target == null || target.HasDied)
            return;

        if (TimeToAttack())
        {

            nextFire = Time.time + (1f / stats.AutoAttackRate);
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

        StatusEffects.DamageTarget(target, stats.AttackDamage);
    }

   

}
