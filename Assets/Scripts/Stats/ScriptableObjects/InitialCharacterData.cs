using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DMT.Characters.Stats
{
    [CreateAssetMenu(fileName ="Character", menuName ="Character/Character Data")]
    public class InitialCharacterData : ScriptableObject
    {
        public string Name;
        public int Level = 1;

        public Sprite Portrait;

        [Header("Initial Stats")]
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