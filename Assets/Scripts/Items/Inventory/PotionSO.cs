using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PotionType
{
    Health,
    Mana,
    Endurance
}

[CreateAssetMenu(fileName = "Potion", menuName = "Item/Potion")]
public class PotionSO : ScriptableObject
{
    public string Name;
    [Space, Header("Potion Stats")]
    public PotionType type;
    public string Description;
    public int Amount;
    public Sprite icon;
   

/*    public override bool Use()
    {
        base.Use();
        switch (type)
        {
            case PotionType.Health:
                return StatusEffects.Heal(stats, Amount);
            case PotionType.Mana:
                Debug.Log("Restoring Mana");
                return true;
            case PotionType.Endurance:
                Debug.Log("Restoring Endurance");
                return true;
            default:
                return false;
            
        }

    }*/

    


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
    
