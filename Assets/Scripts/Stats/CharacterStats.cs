using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour
{
    public CharacterStatsSO Data;

    [SerializeField]
    private int currentHealth;

    [HideInInspector]
    public UnityEvent OnHpLoss;

    public int Health
    {
        get
        {
            return (int)currentHealth;
        }
        set
        {
            currentHealth = value;
            OnHpLoss?.Invoke();
        }
    }

    private void Start()
    {
        Initialize();
    }

    public bool HasDied { get; private set; }
    
    protected virtual void Initialize()
    {
        Data.Initialize();
        Health = Data.MaxHealth;
        HasDied = false;
    }

    public virtual void TakeDamage(float damage)
    {
        
        Health = Mathf.Clamp((int)(Health - damage), 0, Data.MaxHealth);


        if (Data.impactEffect != null)
        {
            //Vector3 impactPosition = transform.position + ImpactEffectOffset + Random.onUnitSphere;
            // GameObject impact = Instantiate(impactEffect, impactPosition, Quaternion.identity);
            //  Destroy(impact, 2f);
        }

        if (Health <= 0 && !HasDied)
        {
            HasDied = true;
            Die();
        }
    }


    protected virtual void Die()
    {
        Debug.Log("Die "  + gameObject);

        //this.enabled = false;
        // gameObject.SetActive(false);
    }

    public bool HasMaxHealth()
    {
        return Health == Data.MaxHealth;
    }
}
