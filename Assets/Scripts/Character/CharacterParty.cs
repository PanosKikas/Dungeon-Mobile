using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem.Utilities;

namespace DMT.Characters
{
    public class CharacterParty : ICollection<Character>
    {
        private const int MaxCharacterPartyCount = 3;
        private readonly ReactiveCollection<Character> party = new();

        public IObservable<CollectionAddEvent<Character>> CharacterAdded => party.ObserveAdd();
        public IObservable<CollectionRemoveEvent<Character>> CharacterRemoved => party.ObserveRemove();

        private readonly Dictionary<Character, List<IDisposable>> characterSubscriptions = new();

        public IEnumerator<Character> GetEnumerator()
        {
            return party.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Character character)
        {
            Assert.IsNotNull(character, "Character added to party was null.");
            Assert.IsFalse(MaxCharacterPartyCount == party.Count);
            if (characterSubscriptions.ContainsKey(character))
            {
                Debug.LogWarning($"Character has already been added to the party.");
                return;
            }

            List<IDisposable> subscriptions = new();
            character.CharacterDied.Subscribe(c => Remove(c)).AddTo(subscriptions);
            characterSubscriptions.Add(character, subscriptions);
            party.Add(character);
        }

        public void Clear()
        {
            party.Clear();
        }

        public bool Contains(Character character)
        {
            return party.Contains(character);
        }

        public void CopyTo(Character[] array, int arrayIndex)
        {
            party.CopyTo(array, arrayIndex);
        }

        public bool Remove(Character character)
        {
            Assert.IsNotNull(character, "Character to remove from party cannot be null.");
            Assert.IsTrue(party.Contains(character));

            if (characterSubscriptions.TryGetValue(character, out var subscriptions))
            {
                subscriptions.DisposeAndClear();
                characterSubscriptions.Remove(character);
            }
            else
            {
                Debug.LogWarning($"Character subscriptions for {character.NameId} have already been disposed");
            }

            if (party.Count == 1)
            {
                Debug.Log("GAME OVER");
            }
            
            return party.Remove(character);
        }

        public int Count => party.Count;
        public bool IsReadOnly => false;
    }
}