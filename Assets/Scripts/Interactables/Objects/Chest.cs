using DMT.Pickups;
using UnityEngine;

namespace DMT.Interactables
{
    public class Chest : Interactable
    {
        [Header("Open/Closed Chest"), SerializeField]
        GameObject openChest;

        [SerializeField] GameObject closedChest;

        [Header("Loot"), SerializeField] public ItemData[] loot;

        [SerializeField] public PickupObject pickupPrefab;

        private readonly ItemFactory itemFactory = new();

        public override void Interact()
        {
            base.Interact();
            OpenChest();
        }

        private void OpenChest()
        {
            openChest.SetActive(true);
            SpawnLoot();
            closedChest.SetActive(false);
            enabled = false;
        }

        private void SpawnLoot()
        {
            foreach (var itemData in loot)
            {
                var item = itemFactory.Create(itemData);
                PickupObject prefab = Instantiate(pickupPrefab, transform.position + new Vector3(0, -.3f, 0),
                    Quaternion.identity);
                prefab.SetPickup(item);
            }
        }
    }
}