using System;
using System.Collections.Generic;
using System.Linq;
using DMT.Battle.UI;
using DMT.Characters;
using DMT.Characters.Inventory;
using DMT.Characters.Stats;
using UnityEngine;
using UnityEngine.Serialization;

namespace DMT.Battle
{
    public class BattleTeam : MonoBehaviour
    {
        [SerializeField, Header("Debugging Start Data")]
        private InitialCharacterData[] debugTeam;

        [SerializeField] private BattleController controller;
        
        private readonly CharacterParty teamParty = new();
        [SerializeField] private BattleCharactersPresenter charactersPresenter;

        [SerializeField] private BattleCharacter[] teamCharacters;

        private readonly List<BattleCharacter> activeTeamCharacters = new();
        [SerializeField] private BattleTeam enemyTeam;

        [SerializeField] private bool useDebugCharacters;
        
        private void Awake()
        {
            if (useDebugCharacters)
            {
                Setup(debugTeam.Select(x => new Character(x, new NullInventory())));
            }
        }

        public void Setup(IEnumerable<Character> characters)
        {
            foreach (var character in characters)
            {
                teamParty.Add(character);
            }
            
            for (var i =0; i < teamParty.Count; ++i)
            {
                var character = teamParty.ElementAt(i);
                teamCharacters.ElementAt(i).Initialize(character, this, enemyTeam);
                activeTeamCharacters.Add(teamCharacters.ElementAt(i));
            }
            controller.SetTeam(this, enemyTeam);
            charactersPresenter.Initialize(this, controller);
        }

        public void BeginBattle()
        {
            controller.BeginBattle();
        }

        public IEnumerable<BattleCharacter> GetCharacters()
        {
            return activeTeamCharacters;
        }
    }
}