/*
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Equipment", menuName = "Pickups/InventoryPickups/Equipment")]
public class EquipmentSO : ScriptableObject
{
    #region StatValuePair
    [System.Serializable]
    public class StatValuePair
    {
        [FormerlySerializedAs("stat")] public StatType statType;
        public float value;

        public StatValuePair(StatType statType, float value)
        {
            this.statType = statType;
            this.value = value;
        }
    }
    #endregion

    [HideInInspector]
    public List<StatValuePair> StatValuePairs;

    List<(Stat, StatModifier)> Modifiers;

    private void OnEnable()
    {
        if (StatValuePairs == null)
            StatValuePairs = new List<StatValuePair>();
    }

    public override bool Use()
    {
        base.Use();
        // Equip - Unequip
        Equip();
        AddModifiers();
        return true;
    }

    void AddModifiers()
    {
        if (Modifiers == null || !Modifiers.Any())
        {
            Modifiers = new List<(Stat, StatModifier)>();
            foreach (var StatValue in StatValuePairs)
            {
                List<Stat> modifiedStat = stats.stats.FindCharacterStats(StatValue.statType);
                foreach (var stat in modifiedStat)
                {
                    StatModifier modifier = new StatModifier(StatValue.value, this);
                    Modifiers.Add((stat, modifier));
                    stat.AddModifier(modifier);
                }
               
            }
        }
        else
        {
            foreach (var StatModifierPair in Modifiers)
            {
                StatModifierPair.Item1.AddModifier(StatModifierPair.Item2);
            }
        }

    }

    public override void Unequip()
    {
        foreach (var ModifierStatPair in Modifiers)
        {
            ModifierStatPair.Item1.RemoveModifier(ModifierStatPair.Item2);
        }
    }


}
*/
