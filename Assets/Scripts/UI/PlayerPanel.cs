using System;
using DMT.UI.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DMT.Characters;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

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

        private readonly ReactiveProperty<Character> currentSelectedCharacter = new();

        [SerializeField]
        private CharacterStatsUI characterStatsUI;

        private readonly Dictionary<int, IDisposable> characterPageSubscriptions = new();
        private readonly List<IDisposable> characterPartySubscriptions = new();
        
        private CharacterParty characterParty;
        
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            characterPageSubscriptions.Values.ToList().DisposeAndClear();
            Hide();
            characterParty = player.characterParty;
            
            var charactersInParty = characterParty.ToArray(); 
            for (var i = 0; i < characterPages.Length; ++i)
            {
                var pageSubscription = characterPages[i].OnShow.AsObservable().Subscribe(CharacterPageSelected);
                characterPageSubscriptions.Add(i, pageSubscription);
                if (i >= charactersInParty.Length)
                {
                    characterTabs[i].Disable();
                    continue;
                }

                var character = charactersInParty[i];
                ConfigureCharacterUI(character, i);
            }

            inventoryUI.InitializeTo(player.Inventory, characterParty);
            characterStatsUI.SetTo(currentSelectedCharacter);
            characterParty.ObserveAdd.Subscribe(CharacterAddedToParty).AddTo(characterPartySubscriptions);
        }

        private void ConfigureCharacterUI(Character character, int index)
        {
            characterTabs[index].Enable();
            characterTabs[index].SetIcon(character.Portrait);
            characterPages[index].Set(character);
        }

        private void CharacterAddedToParty(CollectionAddEvent<Character> addEvent)
        {
            Assert.IsNotNull(addEvent.Value, "Character added was null");
            var index = characterParty.Count - 1;
            ConfigureCharacterUI(addEvent.Value, index);
        }

        private void CharacterPageSelected(Character character)
        {
            currentSelectedCharacter.Value = character;
        }

        public void Show()
        {
            canvasGroup.SetActive(true);
        }

        public void Hide()
        {
            canvasGroup.SetActive(false);
            currentSelectedCharacter.Value = null;
        }

        private void OnDestroy()
        {
            characterPartySubscriptions.DisposeAndClear();
            characterPartySubscriptions.DisposeAndClear();
        }
    }
}
