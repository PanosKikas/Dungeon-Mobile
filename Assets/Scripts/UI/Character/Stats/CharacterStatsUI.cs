using System;
using System.Linq;
using UnityEngine;
using UniRx;

namespace DMT.Characters.UI
{
    public class CharacterStatsUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField] private CharacterStatUI[] statsUI;
        private IDisposable characterDisposable;
        
        public void SetTo(IObservable<Character> selectedCharacter)
        {
            characterDisposable?.Dispose();
            characterDisposable = selectedCharacter.Subscribe(SetToCharacter);
        }
        
        private void SetToCharacter(Character character)
        {
            if (character == null)
            {
                Hide();
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