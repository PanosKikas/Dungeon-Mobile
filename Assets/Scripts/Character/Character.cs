using System;
using DMT.Characters.Stats;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
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

        private readonly IInventory inventory;
        public CharacterClass CharacterClass { get; }

        public Character(InitialCharacterData data, IInventory itemStorage = null)
        {
            inventory = itemStorage;
            CharacterClass = data.CharacterClass;
            NameId = data.name;
            CharacterName = data.CharacterName;
            Level = new ReactiveProperty<int>(data.Level);
            stats = new(data);
            Portrait = data.Portrait;
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

        public void TryUse(IUsable usable)
        {
            Assert.IsNotNull(usable, "Trying to use null item");
            usable.UseOn(this);
            
            if (inventory != null && usable is IStorable storable && inventory.ContainsItem(storable))
            {
                inventory.RemoveItem(storable);
            }
        }
    }
}