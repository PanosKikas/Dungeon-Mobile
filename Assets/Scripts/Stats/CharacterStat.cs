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
        private bool isDirty = true;
        private float baseValue;
        private float _value;

        public StatType StatType { get; }

        StatModifier levelModifier;
        Attribute dependantAttribute;

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

        private void AddModifier()
        {
            AddModifier(levelModifier);
        }

        public void AddModifier(StatModifier mod)
        {
            isDirty = true;
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

        public void AddDependantAttribute(Attribute attribute, StatModifier levelModifier)
        {
            this.levelModifier = levelModifier;
            for (var i = 0; i < attribute.Value; ++i)
            {
                AddModifier(this.levelModifier);
            }

            attribute.OnAttributeChanged.AddListener(AddModifier);
            dependantAttribute = attribute;
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            return FinalStatValue.Subscribe(observer);
        }
    }
}