using System;
using System.Collections.Generic;
using System.Linq;
using DMT.Characters;
using UnityEngine;
using UniRx;
using UnityEngine.Assertions;

namespace DMT.Battle.UI
{
    public class BattleCharactersPresenter : MonoBehaviour
    {
        [SerializeField] private BattleCharacterHUD[] characterHUD;

        private BattleCharacter currentHighlightedCharacter;
        private readonly Dictionary<BattleCharacter, BattleCharacterHUD> charterHUDMapping = new();
        private readonly List<IDisposable> subscriptions = new();
        private int freeHUDIndex;

        public void Initialize(BattleTeam team, BattleController controller)
        {
            subscriptions.DisposeAndClear();
            var teamCharacters = team.GetCharacters().ToArray();
            for (var i = 0; i < characterHUD.Length; ++i)
            {
                if (i >= teamCharacters.Length)
                {
                    characterHUD[i].gameObject.SetActive(false);
                    continue;
                }
                characterHUD[i].Set(teamCharacters[i]);
                charterHUDMapping.Add(teamCharacters[i], characterHUD[i]);
            }

            if (controller is PlayerBattleController playerController)
            {
                playerController.CurrentSelectedCharacter.Subscribe(CurrentSelectedCharacterChanged).AddTo(subscriptions);
            }
        }

        private void CurrentSelectedCharacterChanged(BattleCharacter selectedCharacter)
        {
            if (selectedCharacter == null)
            {
                return;
            }
            
            if (currentHighlightedCharacter != null)
            {
                charterHUDMapping[currentHighlightedCharacter].Unhighlight();
            }
            Highlight(selectedCharacter);
        }

        private void Highlight(BattleCharacter character)
        {
            charterHUDMapping[character].Highlight();
            currentHighlightedCharacter = character;
        }

        private void OnDestroy()
        {
            subscriptions.DisposeAndClear();
        }
    }
}