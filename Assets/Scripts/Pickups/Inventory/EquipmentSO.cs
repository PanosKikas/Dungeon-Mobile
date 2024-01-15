using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DMT.Character.Stats;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Pickups/InventoryPickups/Equipment")]
public class EquipmentSO : EquipableSO
{
    #region StatValuePair
    [System.Serializable]
    public class StatValuePair
    {
        public Stat stat;
        public float value;

        public StatValuePair(Stat stat, float value)
        {
            this.stat = stat;
            this.value = value;
        }
    }
    #endregion

    [HideInInspector]
    public List<StatValuePair> StatValuePairs;

    List<(CharacterStat, StatModifier)> Modifiers;

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
            Modifiers = new List<(CharacterStat, StatModifier)>();
            foreach (var StatValue in StatValuePairs)
            {
                List<CharacterStat> modifiedStat = stats.stats.FindCharacterStats(StatValue.stat);
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
