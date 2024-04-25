using DMT.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item, IUsable
{
    public PotionSO potionData { get; set; }

    public override string Name => potionData.Name;

    public override string Description => potionData.Description;

    public override Sprite Icon => potionData.icon;

    public Potion(PotionSO potionData)
    {
        this.potionData = potionData;
    }

    public void UseOn(Character character)
    {
        switch (potionData.type)
        {
            case PotionType.Health:
                Debug.Log("HEALING " + character.name);
                break;
            case PotionType.Mana:
                Debug.Log("Restoring mana on " + character.name);
                break;
            case PotionType.Endurance:
                Debug.Log("Restoring endurance on " + character.name);
                break;
            default:
                break;
        }
    }
}
