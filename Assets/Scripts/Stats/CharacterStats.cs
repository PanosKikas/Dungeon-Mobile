using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace DMT.Characters.Stats
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

    public class CharacterStats : IEnumerable<CharacterStat>
    {
        public readonly ReactiveProperty<int> CurrentHealth;
        public int CurrentEndurance { get; set; }

        public readonly CharacterStat maxHealthStat;
        public int MaxHealth => (int)maxHealthStat.Value;
        public readonly CharacterStat attackDamageStat;

        public readonly CharacterStat AutoAttackRateStat;
        public readonly CharacterStat MaxEnduranceStat;
        public readonly CharacterStat MaxManaStat;
        public readonly CharacterStat EnduranceRegenStat;
        public readonly CharacterStat CriticalDamageStat;
        public readonly CharacterStat CriticalChanceStat;
        public readonly CharacterStat MagicDamageStat;
        public readonly CharacterStat EvasionChanceStat;
        public readonly CharacterStat PhysicalDefenseStat;
        public readonly CharacterStat MagicalResistanceStat;
        public readonly CharacterStat ItemDropRateStat;

        private readonly Dictionary<StatType, CharacterStat> statsMapping;

        public CharacterStats(InitialCharacterData initialData)
        {
            maxHealthStat = new(initialData.baseMaxHealthStat, StatType.MaxHealth);
            attackDamageStat = new(initialData.baseAttackDamageStat, StatType.AttackDamage);
            AutoAttackRateStat = new(initialData.baseAutoAttackRateStat, StatType.AttackSpeed);
            MaxEnduranceStat = new(initialData.baseMaxEnduranceStat, StatType.MaxEndurance);
            MaxManaStat = new(initialData.baseMaxManaStat, StatType.MaxMana);
            EnduranceRegenStat = new(initialData.baseEnduranceRegenStat, StatType.EnduranceRegen);
            CriticalDamageStat = new(initialData.baseCriticalDamageStat, StatType.CriticalDamage);
            CriticalChanceStat = new(initialData.baseCriticalChanceStat, StatType.CriticalChance);
            MagicDamageStat = new(initialData.baseMagicDamageStat, StatType.MagicDamage);
            EvasionChanceStat = new(initialData.baseEvasionChanceStat, StatType.EvasionChance);
            PhysicalDefenseStat = new(initialData.basePhysicalDefenseStat, StatType.PhysicalDefense);
            MagicalResistanceStat = new(initialData.baseMagicalResistanceStat, StatType.MagicResistance);
            ItemDropRateStat = new(initialData.baseItemDropRateStat, StatType.ItemDropRate);
            
            statsMapping = new Dictionary<StatType, CharacterStat>()
            {
                { maxHealthStat.StatType, maxHealthStat },
                { attackDamageStat.StatType, attackDamageStat },
                { AutoAttackRateStat.StatType, AutoAttackRateStat },
                { MaxEnduranceStat.StatType, MaxEnduranceStat },
                { EnduranceRegenStat.StatType, EnduranceRegenStat },
                { MaxManaStat.StatType, MaxManaStat },
                { CriticalDamageStat.StatType, CriticalDamageStat },
                { EvasionChanceStat.StatType, EvasionChanceStat },
                { CriticalChanceStat.StatType, CriticalChanceStat },
                { ItemDropRateStat.StatType, ItemDropRateStat },
                { MagicDamageStat.StatType, MagicDamageStat },
                { PhysicalDefenseStat.StatType, PhysicalDefenseStat },
                { MagicalResistanceStat.StatType, MagicalResistanceStat }
            };

            CurrentHealth = new ReactiveProperty<int>((int)maxHealthStat.Value);
            CurrentEndurance = (int)MaxEnduranceStat.Value;
        }

        public CharacterStat GetStatOfType(StatType statType)
        {
            if (statsMapping.TryGetValue(statType, out var stat))
            {
                return stat;
            }

            throw new ArgumentException("No stat of type " + statType + " found."); 
        }

        public IEnumerator<CharacterStat> GetEnumerator()
        {
            return statsMapping.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}