using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Character[] characters;
    
    [SerializeField]
    private Inventory _inventory;
    
    public void Pickup(Item item)
    {
        _inventory.TryStore(item);
    }
}
