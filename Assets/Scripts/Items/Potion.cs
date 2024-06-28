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

    public bool CanBeUsedOn(Character character)
    {
        switch (potionData.type)
        {
            case PotionType.Health:
                return !character.IsFullHealth();
            case PotionType.Mana:
            case PotionType.Endurance:
            default:
                return true;
        }
    }

    public void UseOn(Character character)
    {
        switch (potionData.type)
        {
            case PotionType.Health:
                character.Heal(potionData.Amount);
                break;
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
    }
}
