using System.Collections.Generic;
using DMT.Battle;
using DMT.Battle.UI;
using DMT.Characters;
using DMT.Characters.Inventory;
using DMT.Characters.Stats;
using UnityEngine;

namespace DMT.Battle
{
    public class BattleSetup : MonoBehaviour
    {
        [SerializeField] GameObject[] enemyPlaceholders;
        
        [SerializeField, Header("Debugging Start Data")]
        private InitialCharacterData[] debugPlayerCharacters;

        [SerializeField] private BattleController playerController;

        private void Awake()
        {
            SetupBattleScene();
        }

        private void SetupBattleScene()
        {
            SetupPlayerCharacters();
        }
        
        // TODO: Replace once battle transition is implemented.
        private void SetupPlayerCharacters()
        {
            List<Character> playerParty = new List<Character>();
            foreach (var characterData in debugPlayerCharacters)
            {
                playerParty.Add(new Character(characterData, new NullInventory()));
                playerController.Initialize(playerParty);
            }
        }
    }
}
