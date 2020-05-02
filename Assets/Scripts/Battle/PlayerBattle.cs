using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;


public class PlayerBattle : MonoBehaviour, IDamagable
{
    [SerializeField]
    CharacterStats playerStats;

    int currentHp;

    bool isMainHero = true;

    Animator animator;

    GameObject enemyTarget;

    [SerializeField]
    LayerMask enemyLayerMask;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        currentHp = playerStats.MaxHealth;
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FindAttackTarget();
            if (enemyTarget != null)
            {
                animator.SetTrigger("Attack");
                GetComponent<CinemachineImpulseSource>().GenerateImpulse();
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

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }
}
