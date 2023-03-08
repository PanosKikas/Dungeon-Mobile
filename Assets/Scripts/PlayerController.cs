using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public Character Character { get; private set; }
    [SerializeField]
    private CharacterStatsSO initialStats;
    
    [SerializeField]
    private Inventory _inventory;

    private void Start()
    {
        Character = new Character(initialStats);
    }

    public void Pickup(Item item)
    {
        _inventory.TryStore(item);
    }
}
