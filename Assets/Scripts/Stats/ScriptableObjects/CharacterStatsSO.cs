using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DMT.Character.Stats
{
    [CreateAssetMenu(fileName ="Stats", menuName ="Character/Stats")]
    public class CharacterStatsSO : ScriptableObject
    {
        public int baseMaxHealthStat;
        public int baseAttackDamageStat;
        public float baseAutoAttackRateStat;
        public float baseManualAttackRateStat;
        public int baseMaxEnduranceStat;
        public int baseMaxManaStat;
        public float baseEnduranceRegenStat;
        public int baseCriticalDamageStat;
        public float baseCriticalChanceStat;
        public int baseMagicDamageStat;
        public float baseEvasionChanceStat;
        public int basePhysicalDefenseStat;
        public int baseMagicalResistanceStat;
        public float baseItemDropRateStat;
        public float baseMobilityStat;
    }
}