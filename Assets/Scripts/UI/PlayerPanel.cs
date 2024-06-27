using System;
using DMT.UI.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DMT.Characters;
using UniRx;
using UnityEngine;

namespace DMT.UI.Screen
{
    public class PlayerPanel : MonoBehaviour
    {
        [SerializeField]
        private Player player;

        [SerializeField]
        private InventoryPanel inventoryUI;

        private CanvasGroup canvasGroup;

        [SerializeField]
        private CharacterPanel[] characterPages;

        [SerializeField]
        private TabButtonUI[] characterTabs;

        private readonly ReactiveProperty<Character> selectedCharacter = new();

        [SerializeField] private CharacterStatsUI characterStatsUI;

        private readonly List<IDisposable> subscriptions = new();
        
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            subscriptions.DisposeAndClear();
            Hide();
            var charactersInParty = player.Characters.ToArray();
            for (int i = 0; i < characterPages.Length; ++i)
            {
                if (i >= charactersInParty.Length)
                {
                    characterTabs[i].Disable();
                    continue;
                }
                characterTabs[i].SetIcon(charactersInParty[i].Portrait);
                characterPages[i].SubscribeTo(charactersInParty[i]);
                characterPages[i].OnShow.AsObservable().Subscribe(CharacterPageSelected).AddTo(subscriptions);
            }

            inventoryUI.Initialize(player.Inventory, charactersInParty);
            selectedCharacter.Value = charactersInParty.First();
            characterStatsUI.SubscribeTo(selectedCharacter);
        }

        private void CharacterPageSelected(Character character)
        {
            selectedCharacter.Value = character;
        }

        public void Show()
        {
            canvasGroup.SetActive(true);
        }

        public void Hide()
        {
            canvasGroup.SetActive(false);
        }
    }
}
