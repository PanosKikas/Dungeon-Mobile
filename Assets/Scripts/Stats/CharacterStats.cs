using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DMT.Character.Stats
{
    public class CharacterStats
    {
        public int CurrentHealth { get; set; }
        public int CurrentEndurance { get; set; }

        private CharacterStat _maxHealthStat;
        public int MaxHealth => (int)_maxHealthStat.Value;
        private CharacterStat _attackDamageStat;
        public int AttackDamage => (int)_attackDamageStat.Value;
        
        public CharacterStat AutoAttackRateStat;
        public CharacterStat ManualAttackRateStat;
        public CharacterStat MaxEnduranceStat;
        public CharacterStat MaxManaStat;
        public CharacterStat EnduranceRegenStat;
        public CharacterStat CriticalDamageStat;
        public CharacterStat CriticalChanceStat;
        public CharacterStat MagicDamageStat;
        public CharacterStat EvasionChanceStat;
        public CharacterStat PhysicalDefenseStat;
        public CharacterStat MagicalResistanceStat;
        public CharacterStat ItemDropRateStat;

        public CharacterStat MobilityStat;

        public CharacterStats(CharacterStatsSO initialData)
        {
            _maxHealthStat = new(initialData.baseMaxHealthStat);
            _attackDamageStat = new(initialData.baseAttackDamageStat);
            AutoAttackRateStat = new(initialData.baseAutoAttackRateStat);
            ManualAttackRateStat = new(initialData.baseManualAttackRateStat);
            MaxEnduranceStat = new(initialData.baseMaxEnduranceStat);
            MaxManaStat = new(initialData.baseMaxManaStat);
            EnduranceRegenStat = new(initialData.baseEnduranceRegenStat);
            CriticalDamageStat = new(initialData.baseCriticalDamageStat);
            CriticalChanceStat = new(initialData.baseCriticalChanceStat);
            MagicDamageStat = new(initialData.baseMagicDamageStat);
            EvasionChanceStat = new(initialData.baseEvasionChanceStat);
            PhysicalDefenseStat = new(initialData.basePhysicalDefenseStat);
            MagicalResistanceStat = new(initialData.baseMagicalResistanceStat);
            ItemDropRateStat = new(initialData.baseItemDropRateStat);
            MobilityStat = new(initialData.baseMobilityStat);

            CurrentHealth = (int)_maxHealthStat.Value;
            CurrentEndurance = (int)MaxEnduranceStat.Value;
        }

        public virtual void TakeDamage(float damage)
        {
            CurrentHealth = (int)Mathf.Clamp(CurrentHealth - damage, 0, Int32.MaxValue);
        }
    }
}
