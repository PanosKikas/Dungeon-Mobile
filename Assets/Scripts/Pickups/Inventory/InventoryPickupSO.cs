using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryPickupSO : PickupSO
{   

    [TextArea]
    public string description;

    public int SellValue;
    public int StackLimit = 99;

    
}
