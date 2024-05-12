using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DMT.Character.Stats
{
    public enum StatType
    {
        MaxHealth,
        MaxEndurance,
        EnduranceRegen,
        MaxMana,
        AttackDamage,
        AttackSpeed,
        CriticalDamage,
        EvasionChance,
        CriticalChance,
        ItemDropRate,
        MagicDamage,
        PhysicalDefense,
        MagicResistance
    }

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

        public CharacterStats(InitialCharacterData initialData)
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

        public CharacterStat GetStatOfType(StatType statType)
        {
            switch (statType)
            {
                case StatType.MaxHealth:
                    return _maxHealthStat;
                case StatType.MaxEndurance:
                    return MaxEnduranceStat;
                case StatType.EnduranceRegen:
                    return EnduranceRegenStat;
                case StatType.MaxMana:
                    return MaxManaStat;
                case StatType.AttackDamage:
                    return _attackDamageStat;
                case StatType.AttackSpeed:
                    return AutoAttackRateStat;
                case StatType.CriticalDamage:
                    return CriticalDamageStat;
                case StatType.EvasionChance:
                    return EvasionChanceStat;
                case StatType.CriticalChance:
                    return CriticalChanceStat;
                case StatType.ItemDropRate:
                    return ItemDropRateStat;
                case StatType.MagicDamage:
                    return MagicDamageStat;
                case StatType.PhysicalDefense:
                    return PhysicalDefenseStat;
                case StatType.MagicResistance:
                    return MagicalResistanceStat;
                default:
                    throw new ArgumentException("No stat type found " + statType);
            }
        }
    }
}
