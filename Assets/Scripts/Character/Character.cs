using System;
using DMT.Character.Stats;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DMT.Character
{
    public class Character : MonoBehaviour, IDamagable
    {
        public CharacterStats stats { get; private set; }

        [SerializeField] private CharacterStatsSO initialStats;
        public IInventory inventory { get; private set; }
        
        private void Awake()
        {
            stats = new(initialStats);
            inventory = new Inventory();
        }

        public bool IsAlive => stats.CurrentHealth > 0;
        
        public void Pickup(ICollectable item)
        {
            if (item is IStorable storable)
            {
                inventory.TryStore(storable);
            }
            else if (item is IUsable usable)
            {
                usable.UseOn(this);
            }
        }

        private void Die()
        {
            Debug.Log($"Character {gameObject.name} has died ");
            //this.enabled = false;
            // gameObject.SetActive(false);
        }

        public void TakeDamage(int damage)
        {
            stats.CurrentHealth = Mathf.Max((int)(stats.CurrentHealth - damage), 0);

            //Vector3 impactPosition = transform.position + ImpactEffectOffset + Random.onUnitSphere;
            // GameObject impact = Instantiate(impactEffect, impactPosition, Quaternion.identity);
            //  Destroy(impact, 2f);

            if (!IsAlive)
            {
                Die();
            }
        }
    }
}