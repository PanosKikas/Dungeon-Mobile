using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PickupSO : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    
    [TextArea]
    public string description;

    public int SellValue;
    public int StackLimit = 99;
    
    /*[Space, Header("Potion Stats")]
    public PotionType type;*/

    public int Amount;

    public EquipmentType Type;
}
