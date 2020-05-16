using UnityEngine;


public abstract class CharacterStats : ScriptableObject
{
    
    public int CurrentHealth;
    public int MaxHealth = 500;
    
    public int AttackDamage = 50;

    public float AutoAttackRate = .5f;
    public bool HasDied = false;

    public GameObject impactEffect;

    [SerializeField]
    Vector3 ImpactEffectOffset;

    void OnEnable()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        CurrentHealth = MaxHealth;
        
    }


    public bool HasMaxHealth()
    {
        return CurrentHealth == MaxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);

        //OnHpLoss?.Invoke(stats);

        if (impactEffect != null)
        {
            //Vector3 impactPosition = transform.position + ImpactEffectOffset + Random.onUnitSphere;
           // GameObject impact = Instantiate(impactEffect, impactPosition, Quaternion.identity);
          //  Destroy(impact, 2f);
        }

        if (CurrentHealth <= 0 && !HasDied)
        {
            HasDied = true;
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Die");

        //this.enabled = false;
       // gameObject.SetActive(false);
    }
}
