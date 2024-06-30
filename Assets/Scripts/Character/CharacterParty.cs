using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine.Assertions;

namespace DMT.Characters
{
    public class CharacterParty : ICollection<Character>
    {
        private const int MaxCharacterPartyCount = 3;
        private readonly ReactiveCollection<Character> party = new();

        public IObservable<CollectionAddEvent<Character>> CharacterAdded => party.ObserveAdd();
        public IObservable<CollectionRemoveEvent<Character>> CharacterRemoved => party.ObserveRemove();

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
            Assert.IsFalse(MaxCharacterPartyCount == party.Count);
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
            Assert.IsTrue(party.Contains(character));
            return party.Remove(character);
        }

        public int Count => party.Count;
        public bool IsReadOnly => false;
    }
}