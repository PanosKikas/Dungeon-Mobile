using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class StatusEffects : MonoBehaviour, IDamagable
{

    public CharacterStats stats;

    [SerializeField]
    int CurrentHealth;

    void Start()
    {
        CurrentHealth = stats.MaxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        StartCoroutine(Stagger());
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator Stagger()
    {
        AIPath path = GetComponent<AIPath>();
        float speed = path.maxSpeed;
        path.maxSpeed *= .4f;
        yield return new WaitForSeconds(.1f);
        path.maxSpeed = speed;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
