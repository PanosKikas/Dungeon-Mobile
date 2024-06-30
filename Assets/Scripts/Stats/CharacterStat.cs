using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UniRx;
using UnityEngine;

namespace DMT.Characters.Stats
{
    [Serializable]
    public class CharacterStat : IObservable<float>
    {
        private float baseValue;
        
        public StatType StatType { get; }

        private ReactiveProperty<float> FinalStatValue { get; } = new();

        public float Value => FinalStatValue.Value;

        private readonly List<StatModifier> statModifiers;

        public CharacterStat(float value, StatType type)
        {
            baseValue = value;
            StatType = type;
            statModifiers = new List<StatModifier>();
            FinalStatValue.Value = CalculateFinalValue();
        }
        
        public void AddModifier(StatModifier mod)
        {
            statModifiers.Add(mod);
            FinalStatValue.Value = CalculateFinalValue();
        }

        public bool RemoveModifier(StatModifier mod)
        {
            if (!statModifiers.Remove(mod))
            {
                return false;
            }

            FinalStatValue.Value = CalculateFinalValue();
            return true;
        }

        private float CalculateFinalValue()
        {
            var finalValue = baseValue + statModifiers.Sum(modifier => modifier.Value);
            return (float)Math.Round(finalValue, 4);
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            return FinalStatValue.Subscribe(observer);
        }
    }
}