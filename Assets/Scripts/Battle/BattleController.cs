using System;
using System.Collections.Generic;
using System.Linq;
using DMT.Battle.UI;
using DMT.Characters;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace DMT.Battle
{
    public class BattleController : MonoBehaviour
    {
        protected IEnumerable<BattleCharacter> teamCharacters => controlledTeam.GetCharacters();
        protected IEnumerable<BattleCharacter> enemyCharacters => enemyTeam.GetCharacters();
        
        private BattleTeam controlledTeam;
        private BattleTeam enemyTeam;
        
        public void SetTeam(BattleTeam myTeam, BattleTeam otherTeam)
        {
            controlledTeam = myTeam;
            enemyTeam = otherTeam;
        }

        public virtual void BeginBattle()
        {
            foreach (var character in teamCharacters)
            {
                character.CanTick = true;
            }
        }
    }
}