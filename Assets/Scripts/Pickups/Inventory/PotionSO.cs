using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Type
{
    Health,
    Mana,
    Endurance
}

[CreateAssetMenu(fileName = "Potion", menuName = "Pickups/InventoryPickups/Potion")]
public class PotionSO : InventoryPickupSO
{

    [Space, Header("Potion Stats")]
    public Type type;

    public int Amount;
    public override bool Use()
    {
        base.Use();
        switch (type)
        {
            case Type.Health:
                return StatusEffects.Heal(stats, Amount);
            case Type.Mana:
                Debug.Log("Restoring Mana");
                return true;
            case Type.Endurance:
                Debug.Log("Restoring Endurance");
                return true;
            default:
                return false;
            
        }

    }

    
/*    bool RestoreMana()
    {
        if (stats.HasMaxMana())
        {
            return false;
        }
        statusEffects.Heal(Amount);
        return true;

    }*/

    bool ReduceEndurance()
    {
        return true;
    }
}
    
