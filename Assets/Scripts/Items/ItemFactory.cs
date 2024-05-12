using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{
    public ICollectable Create(ItemData scriptableObject)
    {
        if (scriptableObject is EquipmentData)
        {
            return new Equipment((EquipmentData)scriptableObject);
        }
        else if (scriptableObject is PotionData)
        {
            return new Potion((PotionData)scriptableObject);
        }
        else
        {
            return null;
        }
    }
}
