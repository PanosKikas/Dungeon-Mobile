using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Consumable", menuName ="Pickups/Consumables")]
public class ConsumableSO : ScriptableObject
{
    public PickupSO pickupStats;
    public enum Type
    {
        Health,
        Gold
    }

    public Type type;
    public int value;
    
}
