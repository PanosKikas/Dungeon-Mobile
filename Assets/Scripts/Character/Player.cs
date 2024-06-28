using DMT.Characters;
using DMT.Characters.Stats;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private const int MaxCharacterCount = 3;
    public IInventory Inventory { get; private set; }

    public CharacterParty characterParty { get; private set; }

    [SerializeField] private InitialCharacterData[] initialCharacters;

    private void Awake()
    {
        characterParty = new CharacterParty();
        Inventory = new Inventory.Inventory();
        foreach (var characterData in initialCharacters)
        {
            characterParty.Add(new Character(characterData, Inventory));
        }
    }

    public bool Pickup(ICollectable item)
    {
        if (item is IStorable storable && !Inventory.IsFull())
        {
            Inventory.Store(storable);
            return true;
        }

        return false;
    }

    public void TakeDamage(int damage)
    {
        characterParty.First().TakeDamage(damage);
    }

    public bool IsInventoryFull()
    {
        return Inventory.IsFull();
    }
}