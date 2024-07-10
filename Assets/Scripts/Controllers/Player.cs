using DMT.Characters;
using DMT.Characters.Stats;
using System.Linq;
using DMT.Characters.Inventory;
using NUnit.Framework;
using UnityEngine;

namespace DMT.Controllers
{
    public class Player : MonoBehaviour, IDamagable
    {
        public IInventory Inventory { get; private set; }

        public CharacterParty CharacterParty { get; } = new();

        [SerializeField] private InitialCharacterData[] initialCharacters;

        private void Awake()
        {
            Inventory = new Inventory();
            foreach (var characterData in initialCharacters)
            {
                CharacterParty.Add(new Character(characterData, Inventory));
            }
        }

        public void TakeDamage(int damage)
        {
            if (CharacterParty.Any())
            {
                CharacterParty.First().TakeDamage(damage);
            }
        }

        public bool IsInventoryFull()
        {
            return Inventory.IsFull();
        }

        public void AddToParty(InitialCharacterData characterData)
        {
            var character = new Character(characterData, Inventory);
            CharacterParty.Add(character);
        }

        public void RemoveFromParty(Character character)
        {
            Assert.IsFalse(CharacterParty.Count == 1, "Cannot remove last character from party");
            CharacterParty.Remove(character);
        }
    }
}