using System;
using UnityEditor;
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