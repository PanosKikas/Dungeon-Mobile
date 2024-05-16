using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace DMT.Characters.Stats
{
    [System.Serializable]
    public class CharacterStat
    {
        private bool isDirty = true;
        private float baseValue;
        private float _value;

        public StatType StatType { get; private set; }

        StatModifier levelModifier;
        Attribute dependantAttribute;

        public float Value
        {
            get
            {
                if (isDirty)
                {
                    _value = CalculateFinalValue();
                    isDirty = false;
                }

                return _value;
            }
        }

        private readonly List<StatModifier> statModifiers;

        public CharacterStat(float value)
        {
            baseValue = value;
            isDirty = true;
            statModifiers = new List<StatModifier>();
        }

        private void AddModifier()
        {
            AddModifier(levelModifier);
        }

        public void AddModifier(StatModifier mod)
        {
            isDirty = true;
            statModifiers.Add(mod);
        }

        public bool RemoveModifier(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                isDirty = true;
                return true;
            }

            return false;
        }

        private float CalculateFinalValue()
        {
            float finalValue = baseValue;
            for (int i = 0; i < statModifiers.Count; ++i)
            {
                StatModifier modifier = statModifiers[i];

                finalValue += modifier.Value;

            }

            return (float)Math.Round(finalValue, 4);
        }

        public void AddDependantAttribute(Attribute attribute, StatModifier _levelModifier)
        {
            levelModifier = _levelModifier;
            for (int i = 0; i < attribute.Value; ++i)
            {

                AddModifier(levelModifier);
            }

            attribute.OnAttributeChanged.AddListener(AddModifier);
            dependantAttribute = attribute;

        }
    }
}
