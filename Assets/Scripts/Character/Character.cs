using System;
using DMT.Characters.Equipment;
using DMT.Characters.Inventory;
using DMT.Characters.Stats;
using DMT.Pickups;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace DMT.Characters
{
    public class Character : IDamagable
    {
        public string CharacterName { get; }
        public string Id { get; private set; }
        public Sprite Portrait { get; private set; }
        public CharacterStats Stats { get; }
        public CharacterEquipment Equipment { get; }

        public readonly ReactiveProperty<int> Level;

        private readonly IInventory inventory;
        public CharacterClass CharacterClass { get; }

        public readonly ReactiveProperty<float> CurrentHealth;
        public readonly ReactiveProperty<float> CurrentEndurance;

        public IObservable<Character> CharacterDied =>
            CurrentHealth.Where(hp => hp <= 0).Select(_ => this);

        public Character(InitialCharacterData data, IInventory itemStorage)
        {
            inventory = itemStorage;
            CharacterClass = data.CharacterClass;
            Id = data.name;
            CharacterName = data.CharacterName;
            Level = new ReactiveProperty<int>(data.Level);
            Stats = new CharacterStats(data);
            Portrait = data.Portrait;
            Equipment = new CharacterEquipment(this, inventory);
            CurrentHealth = new ReactiveProperty<float>(Stats.MaxHealthStat.Value);
            CurrentEndurance = new ReactiveProperty<float>(Stats.MaxEnduranceStat.Value);
        }

        public bool IsAlive => CurrentHealth.Value > 0;

        private void Die()
        {
            Debug.Log($"Character {CharacterName} has died ");
        }

        public bool IsFullHealth()
        {
            return (int)CurrentHealth.Value == Stats.MaxHealth;
        }

        public void Heal(int amount)
        {
            CurrentHealth.Value = Mathf.Min(CurrentHealth.Value + amount, Stats.MaxHealth);
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth.Value = Mathf.Max(CurrentHealth.Value - damage, 0);

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

        public void Use(IUsable usable)
        {
            Assert.IsNotNull(usable, "Trying to use null item");
            usable.UseOn(this);

            if (inventory != null && usable is IStorable storable)
            {
                inventory.RemoveItem(storable);
            }
        }
    }
}