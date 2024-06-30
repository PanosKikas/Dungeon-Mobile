using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{
    public ICollectable Create(ItemData scriptableObject)
    {
        return scriptableObject switch
        {
            EquipmentData data => new Equipment(data),
            PotionData data => new Potion(data),
            _ => null
        };
    }
}