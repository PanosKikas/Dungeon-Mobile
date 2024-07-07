using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DMT.Characters;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DMT.Battle
{
    public class PlayerBattleController : BattleController
    {
        private RaycastHit2D[] results = new RaycastHit2D[1];
        public ReactiveProperty<BattleCharacter> CurrentSelectedCharacter { get; } = new();

        private BattleCharacter SelectedCharacter => CurrentSelectedCharacter.Value;

        [SerializeField] private LayerMask characterLayerMask;
        
        public override void BeginBattle()
        {
            base.BeginBattle();
            Possess(teamCharacters.First());
        }

        private void Possess(BattleCharacter character)
        {
            CurrentSelectedCharacter.Value?.Unpossess();
            character.Possess();
            CurrentSelectedCharacter.Value = character;
        }

        public void CharacterClicked(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }
            
            var clickPos = context.ReadValue<Vector2>();
            var battleCharacter = GetCharacterClicked(clickPos);

            if (battleCharacter == null || battleCharacter == SelectedCharacter || !battleCharacter.IsAlive())
            {
                return;
            }
            
            var teammateClicked = teamCharacters.Contains(battleCharacter);
            if (teammateClicked)
            {
                Possess(battleCharacter);
            }
            else
            {
                SelectedCharacter.ManualAttack(battleCharacter);
            }
        }

        private BattleCharacter GetCharacterClicked(Vector2 cursorPosition)
        {
            var ray = Camera.main.ScreenPointToRay(cursorPosition);

            var numResults = Physics2D.GetRayIntersectionNonAlloc(ray, results, 2f, characterLayerMask);
            if (numResults <= 0)
            {
                return null;
            }

            var hitResult = results[0];
            return hitResult.collider.GetComponent<BattleCharacter>();
        }
    }
}