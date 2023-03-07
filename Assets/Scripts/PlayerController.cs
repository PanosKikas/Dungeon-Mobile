using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterStatsSO InitialStats;
    private CharacterStats _stats;

    [SerializeField]
    private Inventory _inventory;
    
    private void Start()
    {
        _stats = new CharacterStats(InitialStats);
    }

    public void Pickup(Item item)
    {
        if (item is IStorable storable)
        {
            _inventory.TryStoreInventory(item);
        }
    }
}
