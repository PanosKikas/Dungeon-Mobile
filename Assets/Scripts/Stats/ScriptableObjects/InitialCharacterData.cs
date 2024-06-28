using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

namespace DMT.Characters.Stats
{
    [CreateAssetMenu(fileName ="Character", menuName ="Character/Character Data")]
    public class InitialCharacterData : ScriptableObject
    {
        public string CharacterName;
        public int Level = 1;
        public CharacterClass CharacterClass = CharacterClass.Gunslinger;
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