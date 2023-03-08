using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionType
{
    Heal,
    Divinity
}

public class Potion : Item, IStorable, IUsable
{
    private PotionType type;
    
    public bool TryUse()
    {
        _character.Heal(20);
        return true;
    }

    public Potion(PickupSO data) : base(data)
    {
        PotionSO potionData = data as PotionSO;
        if (potionData != null)
        {
            type = potionData.Type;
        }
    }
}
