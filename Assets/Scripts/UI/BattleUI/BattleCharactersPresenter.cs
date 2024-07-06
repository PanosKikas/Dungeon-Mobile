using System;
using System.Collections.Generic;
using System.Linq;
using DMT.Characters;
using UnityEngine;
using UniRx;
using UnityEngine.Assertions;

namespace DMT.Battle.UI
{
    public class BattleCharactersPresenter : MonoBehaviour
    {
        [SerializeField] private BattleCharacterHUD[] characterHUD;

        private readonly List<IDisposable> subscriptions = new();
        private int freeHUDIndex;

        public void Initialize(BattleTeam team)
        {
            var teamCharacters = team.GetCharacters().ToArray();
            for (int i = 0; i < teamCharacters.Length; ++i)
            {
                characterHUD[i].Set(teamCharacters[i]);
            }
        }

        private void OnDestroy()
        {
            subscriptions.DisposeAndClear();
        }
    }
}