using System;
using System.Collections.Generic;
using System.Linq;
using DMT.Characters;
using NUnit.Framework;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DMT.Battle
{
    public class PlayerBattleController : BattleController
    {
        private readonly RaycastHit2D[] raycastResults = new RaycastHit2D[1];
        private Camera mainCamera;
        public ReactiveProperty<BattleCharacter> CurrentSelectedCharacter { get; } = new();

        private BattleCharacter SelectedCharacter => CurrentSelectedCharacter.Value;

        [SerializeField] private LayerMask characterLayerMask;

        private readonly List<IDisposable> subscriptions = new();
        
        public override void BeginBattle()
        {
            base.BeginBattle();
            subscriptions.DisposeAndClear();
            foreach (var character in teamCharacters)
            {
                character.CharacterDied.Subscribe(CharacterHasDied).AddTo(subscriptions);
            }
            Possess(teamCharacters.First());
        }

        private void CharacterHasDied(BattleCharacter character)
        {
            if (character == SelectedCharacter)
            {
                var aliveTeammate = teamCharacters.FirstOrDefault(c => c.IsAlive());
                if (aliveTeammate == null)
                {
                    CurrentSelectedCharacter.Value?.Unpossess();
                    CurrentSelectedCharacter.Value = null;
                    return;
                }
                Possess(aliveTeammate);
            }
        }

        private void Possess(BattleCharacter character)
        {
            Assert.IsNotNull(character, "Cannot possess null character.");
            CurrentSelectedCharacter.Value?.Unpossess();
            character.Possess();
            CurrentSelectedCharacter.Value = character;
        }

        public void CharacterClicked(InputAction.CallbackContext context)
        {
            if (SelectedCharacter == null || !SelectedCharacter.CanTick)
            {
                return;
            }
            
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
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
            
            Assert.IsNotNull(mainCamera, "Main camera found null");
            var ray = mainCamera.ScreenPointToRay(cursorPosition);
            var numResults = Physics2D.GetRayIntersectionNonAlloc(ray, raycastResults, 2f, characterLayerMask);
            if (numResults <= 0)
            {
                return null;
            }

            var hitResult = raycastResults[0];
            return hitResult.collider.GetComponent<BattleCharacter>();
        }

        private void OnDestroy()
        {
            subscriptions.DisposeAndClear();
        }
    }
}