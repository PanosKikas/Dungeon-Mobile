using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DMT.Pickups
{
    public class ItemFactory
    {
        public IPickable Create(ItemData scriptableObject)
        {
            return scriptableObject switch
            {
                EquipmentData data => new Equipment(data),
                PotionData data => new Potion(data),
                _ => null
            };
        }
    }
}