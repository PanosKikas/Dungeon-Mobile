using System;
using System.Collections.Generic;
using System.Linq;
using DMT.Characters;
using DMT.Characters.Inventory;
using DMT.Characters.Stats;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace DMT.Battle
{
    public class BattleController : MonoBehaviour
    {
        public readonly CharacterParty CharacterParty = new();
        private ReactiveProperty<Character> CurrentSelectedCharacter { get; } = new();

        public void Initialize(IEnumerable<Character> startingCharacters)
        {
            foreach (var character in startingCharacters)
            {
                CharacterParty.Add(character);
            }

            CurrentSelectedCharacter.Value = CharacterParty.First();
        }
    }
}