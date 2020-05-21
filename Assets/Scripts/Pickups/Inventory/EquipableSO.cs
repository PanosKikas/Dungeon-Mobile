using System.Collections;
using System.Collections.Generic;
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

    [HideInInspector]
    public List<StatValuePair> Modifiers;

    private void OnEnable()
    {
        if (Modifiers == null)
            Modifiers = new List<StatValuePair>();
    }

    public override bool Use()
    {
        base.Use();
        // Equip - Unequip
        Equip();
        return true;
    }

    void Equip()
    { 
       CharacterEquipment.Instance.Equip(this);
      
    }
     
}
