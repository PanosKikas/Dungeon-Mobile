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
    public IEnumerable<Character> Characters => characters.Where(c => c != null);
    
    private readonly Character[] characters = new Character[MaxCharacterCount];

    [SerializeField]
    private InitialCharacterData[] initialCharacters;

    private void Awake()
    {
        Inventory = new Inventory();
        for (int i = 0; i < initialCharacters.Length; ++i)
        {
            characters[i] = new Character(initialCharacters[i], Inventory);
        }
    }

    public void Pickup(ICollectable item)
    {
        if (item is IStorable storable)
        {
            Inventory.TryStore(storable);
        }
        else if (item is IUsable usable)
        {
            usable.TryUseOn(characters.First());
        }
    }

    public void TakeDamage(int damage)
    {
        characters.First().TakeDamage(damage);
    }
}
