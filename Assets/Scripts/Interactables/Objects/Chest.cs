using System;
using DMT.Pickups;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace DMT.Interactables
{
    public class Chest : Interactable
    {
        [Header("Open/Closed Chest"), SerializeField]
        GameObject openChest;

        [SerializeField] GameObject closedChest;

        [Header("Loot"), SerializeField] public ItemData[] loot;

        // TODO: Inject when DI container is added.
        [SerializeField] private ItemSpawner itemSpawner;

        [SerializeField] private float spawnPositionVariance = 0.5f;

        private void Start()
        {
            Assert.IsNotNull(itemSpawner, "Item spawner in chest is null");
        }

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
                var spawnPosition = transform.position + new Vector3(0, -.3f, 0)
                                                       + Vector3.right * Random.onUnitSphere.x * spawnPositionVariance;
                itemSpawner.Spawn(itemData, spawnPosition);
            }
        }
    }
}