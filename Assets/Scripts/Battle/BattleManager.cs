using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DMT.Characters;
using DMT.Persistent;
using UnityEngine;
using UniRx;

namespace DMT.Battle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private BattleTeam playerTeam;
        [SerializeField] private BattleTeam enemyTeam;

        [SerializeField] private float startBattleDelaySeconds = 2f;
        
        public static BattleManager Instance { get; private set; }

        private readonly List<IDisposable> battleOverSubscriptions = new();
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void SetupBattle(IEnumerable<Character> playerCharacters, IEnumerable<Character> enemyCharacters)
        {
            battleOverSubscriptions.DisposeAndClear();
            playerTeam.Setup(playerCharacters);
            List<Character> enemies = enemyCharacters.ToList();
            enemyTeam.Setup(enemies);

            foreach (var enemy in enemies)
            {
                enemy.CharacterDied.Subscribe(_ => CheckIfBattleIsOver()).AddTo(battleOverSubscriptions);
            }
        }
        

        public async UniTask BeginBattle()
        {
            await UniTask.WaitForSeconds(startBattleDelaySeconds);
            playerTeam.BeginBattle();
            enemyTeam.BeginBattle();
        }

        private void CheckIfBattleIsOver()
        {
            if (enemyTeam.GetCharacters().Any(c => c.IsAlive()))
            {
                return;
            }

            SceneTransitionManager.Instance.TransitionBackFromBattle().Forget();
        }
    }
}