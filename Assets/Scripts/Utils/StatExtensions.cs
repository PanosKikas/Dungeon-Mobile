using System;
using DMT.Characters.Stats;

namespace Utils
{
    public static class StatExtensions
    {
        public static string ToShortString(this StatType statType)
        {
            switch (statType)
            {
                case StatType.MaxHealth:
                    return "MAX HP";
                case StatType.MaxEndurance:
                    return "MAX ENDR";
                case StatType.EnduranceRegen:
                    return "ENDR REGEN";
                case StatType.AttackSpeed:
                    return "ATK SPD";
                case StatType.AttackDamage:
                    return "ATK DMG";
                case StatType.CriticalDamage:
                    return "CRIT DMG";
                case StatType.EvasionChance:
                    return "EVASION %";
                case StatType.CriticalChance:
                    return "CRIT %";
                case StatType.ItemDropRate:
                    return "DROP %";
                case StatType.MagicDamage:
                    return "MGK DMG";
                case StatType.MaxMana:
                    return "MAX MP";
                case StatType.MagicResistance:
                    return "MGK RES";
                case StatType.PhysicalDefense:
                    return "PHYS DEF";
                default:
                    throw new ArgumentException($"No stat of type {statType} found");
            }
        }
    }
}