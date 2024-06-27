using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using System.Text;
using DMT.Characters.Stats;

namespace DMT.UI.Screen
{
    public class CharacterStatsUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        private readonly List<IDisposable> subscriptions = new();

        [SerializeField] private CharacterStatUI[] statsUI;
        
        public void SubscribeTo(CharacterStats characterStats)
        {
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
            subscriptions.DisposeAndClear();
            canvasGroup.SetActive(true);
        }

        public void Hide()
        {
            canvasGroup.SetActive(false);
        }
    }
}