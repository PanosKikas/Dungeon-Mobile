using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public string Name;
    public string Description;
    public Sprite Icon;
    protected Character _character;

    public Item(PickupSO data)
    {
        Name = data.Name;
        Description = data.Description;
        Icon = data.Icon;
    }
}
