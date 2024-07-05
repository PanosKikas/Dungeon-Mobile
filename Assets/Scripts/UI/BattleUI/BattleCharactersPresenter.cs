using System;
using DMT.Characters;
using TMPro;
using UnityEngine;
using UniRx;

namespace DMT.Battle.UI
{
    public class BattleCharactersPresenter : MonoBehaviour
    {
        [SerializeField] private BattleController controller;
        [SerializeField] private BattleCharacterHUD[] characterHUD;

        private int freeHUDIndex;
        
        private void Awake()
        {
            controller.CharacterParty.CharacterAdded.Subscribe(e => InitializeCharacter(e.Value));
        }

        private void InitializeCharacter(Character character)
        {
            characterHUD[freeHUDIndex].Set(character);
            ++freeHUDIndex;
        }
    }
}