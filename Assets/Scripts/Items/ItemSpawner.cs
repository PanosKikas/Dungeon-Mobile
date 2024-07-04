using System.Collections;
using System.Collections.Generic;
using DMT.Interactables;
using UnityEngine;

namespace DMT.Pickups
{
    public class ItemSpawner : MonoBehaviour, IITemSpawner
    {
        [SerializeField] 
        private PickupObject itemPrefab;
        
        public void Spawn(ItemData itemData, Vector3 position)
        {
            var pickable = Create(itemData);
            var pickupInstance = Instantiate(itemPrefab, position, Quaternion.identity);
            pickupInstance.SetPickup(pickable);
        }
        
        private IPickable Create(ItemData itemData)
        {
            return itemData switch
            {
                EquipmentData data => new Equipment(data),
                PotionData data => new Potion(data),
                _ => null
            };
        }
    }
}