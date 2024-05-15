using DMT.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item, IUsable
{
    public PotionData potionData { get; set; }

    public override string Name => potionData.Name;

    public override string Description => potionData.Description;

    public override Sprite Icon => potionData.Icon;

    public Potion(PotionData potionData)
    {
        this.potionData = potionData;
    }

    public bool TryUseOn(Character character)
    {
        switch (potionData.type)
        {
            case PotionType.Health:
                if (character.IsFullHealth())
                {
                    return false;
                }
                character.Heal(potionData.Amount);
                return true;
            case PotionType.Mana:
                Debug.Log("Restoring mana on " + character);
                break;
            case PotionType.Endurance:
                Debug.Log("Restoring endurance on " + character);
                break;
            default:
                Debug.LogError("No potion of type " + potionData.type + " found.");
                break;
        }
        return true;
    }
}
