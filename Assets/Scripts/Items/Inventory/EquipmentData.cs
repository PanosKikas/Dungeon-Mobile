using System.Collections.Generic;
using DMT.Characters.Stats;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Item/Equipment")]
public class EquipmentData : ItemData
{
    public EquipmentType EquipmentType;

    #region StatValuePair
    [System.Serializable]
    public class StatValuePair
    {
        public StatType StatType;
        public float Value;

        public StatValuePair(StatType stat, float value)
        {
            this.StatType = stat;
            this.Value = value;
        }
    }
    #endregion

    [HideInInspector]
    public List<StatValuePair> Modifiers;
}

public enum EquipmentType
{
    Head = 0,
    Chest = 1,
    Legs = 2,
    Weapon = 3,
    Ring = 4
}