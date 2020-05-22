using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EquipmentType
{
    Head,
    Chest,
    Legs,
    Weapon,
    Potion
}

[CreateAssetMenu(fileName ="Equipment", menuName ="Pickups/InventoryPickups/Equipment")]
public class EquipableSO : InventoryPickupSO
{

    public EquipmentType Type;

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

    void Equip()
    { 
       CharacterEquipment.Instance.Equip(this);  
    }

    public void Unequip()
    {
        foreach (var ModifierStatPair in Modifiers)
        {
            ModifierStatPair.Item1.RemoveModifier(ModifierStatPair.Item2);
        }
    }

    void AddModifiers()
    {
        if (Modifiers == null || !Modifiers.Any())
        {
            Modifiers = new List<(CharacterStat, StatModifier)>();
            foreach (var StatValue in StatValuePairs)
            {
                CharacterStat modifiedStat = stats.FindCharacterStat(StatValue.stat);
                StatModifier modifier = new StatModifier(StatValue.value, this);
                Modifiers.Add((modifiedStat, modifier));
                modifiedStat.AddModifier(modifier);
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
     
}
