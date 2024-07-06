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
        protected ReactiveProperty<BattleCharacter> CurrentSelectedCharacter { get; } = new();

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
    }
}
