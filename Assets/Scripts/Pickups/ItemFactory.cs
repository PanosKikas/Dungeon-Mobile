using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemFactory
{
    public static Item CreateItemFromData(PickupSO data)
    {
        var className = data.GetType().Name.Replace("SO", "");
        var type = Type.GetType(className);
        if (type == null)
        {
            Debug.LogError("Could not create item of type: " + className);
            return null;
        }
        
        Item item = Activator.CreateInstance(type, args: data) as Item;
        return item;
    }
}
