using System;
using DMT.Characters.Stats;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DMT.Characters
{
    public class Character : IDamagable
    {
        public string Name { get; private set; }
        public CharacterStats stats { get; private set; }

        public CharacterEquipment Equipment { get; private set; }

        public int Level { get; private set; } = 1;

        public Character(InitialCharacterData initialStats, IInventory inventory = null)
        {
            Name = initialStats.Name;
            Level = initialStats.Level;
            stats = new(initialStats);
            Equipment = new CharacterEquipment(this, inventory);
        }

        public bool IsAlive => stats.CurrentHealth > 0;
        
        private void Die()
        {
            Debug.Log($"Character has died ");
        }

        public bool IsFullHealth()
        {
            return stats.CurrentHealth == stats.MaxHealth;
        }

        public void Heal(int amount)
        {
            stats.CurrentHealth = Mathf.Min((int)stats.CurrentHealth + amount, stats.MaxHealth);
        }

        public void TakeDamage(int damage)
        {
            stats.CurrentHealth = Mathf.Max((int)(stats.CurrentHealth - damage), 0);

            if (!IsAlive)
            {
                Die();
            }
        }

        public void Equip(IEquipable equipable)
        {
            Equipment.Equip(equipable);
        }

        public void UnequipFrom(EquipmentSlot slot)
        {
            Equipment.Unequip(slot);
        }
    }
}