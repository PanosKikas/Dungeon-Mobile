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
        protected IEnumerable<BattleCharacter> teamCharacters => team.GetCharacters();

        private BattleTeam team;
        
        public void SetTeam(BattleTeam battleTeam)
        {
            team = battleTeam;
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