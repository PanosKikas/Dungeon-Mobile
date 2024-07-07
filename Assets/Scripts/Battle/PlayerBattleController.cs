using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DMT.Characters;
using UniRx;
using UnityEngine;

namespace DMT.Battle
{
    public class PlayerBattleController : BattleController
    {
        public ReactiveProperty<BattleCharacter> CurrentSelectedCharacter { get; } = new();

        private BattleCharacter SelectedCharacter => CurrentSelectedCharacter.Value;
        
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

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectedCharacter.ManualAttack(enemyCharacters.First());
            }
        }
        
    }
}
