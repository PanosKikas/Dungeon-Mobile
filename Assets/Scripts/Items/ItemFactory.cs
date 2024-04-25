using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{
    public ICollectable Create(ScriptableObject scriptableObject)
    {
        if (scriptableObject is EquipmentData)
        {
            return new Equipment((EquipmentData)scriptableObject);
        }
        else if (scriptableObject is PotionSO)
        {
            return new Potion((PotionSO)scriptableObject);
        }
        else
        {
            return null;
        }
    }
}
