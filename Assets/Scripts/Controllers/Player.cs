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

        public CharacterParty characterParty { get; private set; }

        [SerializeField] private InitialCharacterData[] initialCharacters;

        private void Awake()
        {
            characterParty = new CharacterParty();
            Inventory = new Inventory();
            foreach (var characterData in initialCharacters)
            {
                characterParty.Add(new Character(characterData, Inventory));
            }
        }

        public void TakeDamage(int damage)
        {
            if (characterParty.Any())
            {
                characterParty.First().TakeDamage(damage);
            }
        }

        public bool IsInventoryFull()
        {
            return Inventory.IsFull();
        }

        public void AddToParty(InitialCharacterData characterData)
        {
            var character = new Character(characterData, Inventory);
            characterParty.Add(character);
        }

        public void RemoveFromParty(Character character)
        {
            Assert.IsFalse(characterParty.Count == 1, "Cannot remove last character from party");
            characterParty.Remove(character);
        }
    }
}