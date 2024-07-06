using System.Collections.Generic;
using System.Linq;
using DMT.Battle;
using DMT.Battle.UI;
using DMT.Characters;
using DMT.Characters.Inventory;
using DMT.Characters.Stats;
using UnityEngine;

namespace DMT.Battle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private BattleTeam playerTeam;
        [SerializeField] private BattleTeam enemyTeam;
        
        private void Start()
        {
            BeginBattle();
        }

        private void BeginBattle()
        {
            playerTeam.BeginBattle();
            enemyTeam.BeginBattle();
        }
    }
}