using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattle : MonoBehaviour, IDamagable
{
    Animator animator;

    [SerializeField]
    CharacterStats enemyStats;

    [SerializeField]
    PlayerBattle playerTarget;

    IDamagable currentTarget;

    float rateOfFire = .5f;
    float nextFire = 0f;

    int currentHp;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        currentHp = enemyStats.MaxHealth;
        currentTarget = playerTarget.GetComponent<IDamagable>();
    }

    private void Update()
    {
        if (Time.time >= nextFire)
        {
            nextFire = Time.time + 1 / rateOfFire;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length +
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        
        DamageTarget();
    }

    void DamageTarget()
    {
        currentTarget.TakeDamage(enemyStats.MainAttackDamage);
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }
}
