using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class PickupSO : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    
    [FormerlySerializedAs("description")] [TextArea]
    public string Description;

    public int SellValue;
    public int StackLimit = 99;
}
