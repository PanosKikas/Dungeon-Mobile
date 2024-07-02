using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PotionType
{
    Health,
    Mana,
    Endurance
}

[CreateAssetMenu(fileName = "Potion", menuName = "Item/Potion")]
public class PotionData : ItemData
{
    [Space, Header("Potion Stats")]
    public PotionType type;
    public int Amount;
}
    
