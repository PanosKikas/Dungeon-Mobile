using System;
using DMT.UI.Components;
using System.Collections.Generic;
using System.Linq;
using DMT.Characters.Inventory.UI;
using DMT.Controllers;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace DMT.Characters.UI
{
    public class PlayerPanel : MonoBehaviour
    {
        [SerializeField] private Player player;

        [SerializeField] private InventoryPanel inventoryUI;

        private CanvasGroup canvasGroup;

        [FormerlySerializedAs("freeCharacterPages")] [FormerlySerializedAs("characterPages")] [SerializeField]
        private CharacterPanel[] allCharacterPages;

        [SerializeField] private TabButtonUI[] characterTabs;

        [SerializeField] private CharacterManagementPopup characterManagementPopup;

        private readonly ReactiveProperty<Character> currentSelectedCharacter = new();

        [SerializeField] private CharacterStatsUI characterStatsUI;

        private readonly List<IDisposable> characterPageSubscriptions = new();
        private readonly List<IDisposable> characterPartySubscriptions = new();

        private Stack<int> freeCharacterPagesIndexes = new();
        private readonly Dictionary<Character, int> characterPagesMapping = new();

        private CharacterParty characterParty;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            Hide();
            characterParty = player.characterParty;
            freeCharacterPagesIndexes = new Stack<int>(Enumerable.Range(0, allCharacterPages.Length).Reverse());
            var charactersInParty = characterParty.ToArray();

            foreach (var character in charactersInParty)
            {
                AddCharacterUI(character);
            }
            
            for (var i = 0; i < allCharacterPages.Length; ++i)
            {
                allCharacterPages[i].OnShow.AsObservable().Subscribe(CharacterPageSelected)
                    .AddTo(characterPageSubscriptions);
                if (freeCharacterPagesIndexes.Contains(i))
                {
                    characterTabs[i].Disable();
                }
            }
            
            inventoryUI.InitializeTo(player.Inventory, characterParty);
            characterStatsUI.SetTo(currentSelectedCharacter);
            characterParty.CharacterAdded.Subscribe(CharacterAddedToParty).AddTo(characterPartySubscriptions);
            characterParty.CharacterRemoved.Subscribe(CharacterRemovedFromParty).AddTo(characterPartySubscriptions);
        }

        private void AddCharacterUI(Character character)
        {
            var index = freeCharacterPagesIndexes.Pop();
            characterTabs[index].SetIcon(character.Portrait);
            characterTabs[index].Enable();
            characterTabs[index].transform.SetSiblingIndex(characterParty.Count);
            allCharacterPages[index].Set(player, character);
            characterPagesMapping.Add(character, index);
        }

        private void RemoveCharacterUI(Character character, int index)
        {
            characterTabs[index].Disable();
            characterTabs[index].transform.SetSiblingIndex(characterParty.Count + 1);
            allCharacterPages[index].Clear();
            freeCharacterPagesIndexes.Push(index);
            characterPagesMapping.Remove(character);
        }

        private void CharacterAddedToParty(CollectionAddEvent<Character> addEvent)
        {
            Assert.IsNotNull(addEvent.Value, "Character added was null");
            AddCharacterUI(addEvent.Value);
        }

        private void CharacterRemovedFromParty(CollectionRemoveEvent<Character> removeEvent)
        {
            Assert.IsNotNull(removeEvent.Value, "Character removed was null");
            var removedCharacter = removeEvent.Value;

            if (characterPagesMapping.TryGetValue(removedCharacter, out var index))
            {
                RemoveCharacterUI(removedCharacter, index);
            }
            else
            {
                Debug.LogError("No character page found in character mappings");
            }

            SelectValidCharacterTab(removedCharacter);
        }

        private void SelectValidCharacterTab(Character removedCharacter)
        {
            if (currentSelectedCharacter.Value != null && currentSelectedCharacter.Value == removedCharacter)
            {
                for (var i = 0; i < allCharacterPages.Length; ++i)
                {
                    if (!freeCharacterPagesIndexes.Contains(i))
                    {
                        characterTabs[i].OnPointerClick(null);
                        break;
                    }
                }      
            }
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
            characterPageSubscriptions.DisposeAndClear();
            characterPartySubscriptions.DisposeAndClear();
        }
    }
}