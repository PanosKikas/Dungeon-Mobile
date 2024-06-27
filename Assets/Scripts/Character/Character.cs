using System;
using DMT.Characters.Stats;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DMT.Characters
{
    public class Character : IDamagable
    {
        public string CharacterName { get; private set; }
        public string NameId { get; private set; }
        public Sprite Portrait { get; private set; }
        public CharacterStats stats { get; }

        public CharacterEquipment Equipment { get; }

        public readonly ReactiveProperty<int> Level;

        public Character(InitialCharacterData initialStats, IInventory inventory = null)
        {
            NameId = initialStats.name;
            CharacterName = initialStats.CharacterName;
            Level = new ReactiveProperty<int>(initialStats.Level);
            stats = new(initialStats);
            Portrait = initialStats.Portrait;
            Equipment = new CharacterEquipment(this, inventory);
        }

        public bool IsAlive => stats.CurrentHealth.Value > 0;

        private void Die()
        {
            Debug.Log($"Character has died ");
        }

        public bool IsFullHealth()
        {
            return stats.CurrentHealth.Value == stats.MaxHealth;
        }

        public void Heal(int amount)
        {
            stats.CurrentHealth.Value = Mathf.Min(stats.CurrentHealth.Value + amount, stats.MaxHealth);
        }

        public void TakeDamage(int damage)
        {
            stats.CurrentHealth.Value = Mathf.Max((stats.CurrentHealth.Value - damage), 0);

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