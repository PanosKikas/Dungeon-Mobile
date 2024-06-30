using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using System.Text;
using DMT.Characters;
using DMT.Characters.Stats;
using UniRx;

namespace DMT.UI.Screen
{
    public class CharacterStatsUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField] private CharacterStatUI[] statsUI;
        private IDisposable characterDisposable;
        
        public void SubscribeTo(IObservable<Character> selectedCharacter)
        {
            characterDisposable?.Dispose();
            characterDisposable = selectedCharacter.Subscribe(SetToCharacter);
        }
        
        private void SetToCharacter(Character character)
        {
            if (character == null)
            {
                return;
            }

            var characterStats = character.Stats;
            
            if (characterStats.Count() != statsUI.Length)
            {
                throw new InvalidOperationException("Stats UI does not have same number of elements as data.");
            }
            
            var i = 0;
            foreach (var stat in characterStats)
            {
                statsUI[i].SubscribeTo(stat);
                ++i;
            }
        }
        
        public void ShowPanel()
        {
            canvasGroup.SetActive(true);
        }

        public void Hide()
        {
            canvasGroup.SetActive(false);
        }

        private void OnDestroy()
        {
            characterDisposable?.Dispose();
            characterDisposable = null;
        }
    }
}