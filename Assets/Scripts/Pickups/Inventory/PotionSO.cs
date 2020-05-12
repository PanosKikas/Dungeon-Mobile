using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Potion", menuName ="Pickups/InventoryPickups/Potion")]
public class PotionSO : InventoryPickupSO
{
    public enum Type
    {
        Health,
        Mana,
        Endurance
    }

    public Type type;
}
