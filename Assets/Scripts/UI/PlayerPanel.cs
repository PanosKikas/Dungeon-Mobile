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
        [SerializeField] private CharacterPanel[] allCharacterPages;
        [SerializeField] private CharacterStatsUI characterStatsUI;
        [SerializeField] private TabButtonUI[] characterTabs;

        private CanvasGroup canvasGroup;
        private bool isVisible;
        private Stack<int> freeCharacterPagesIndexes = new();
        private readonly Dictionary<Character, int> usedCharacterPagesMapping = new();
        private readonly ReactiveProperty<Character> currentSelectedCharacter = new();
        
        private readonly List<IDisposable> characterPageSubscriptions = new();
        private readonly List<IDisposable> characterPartySubscriptions = new();

        private CharacterParty characterParty;
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            Hide();
            characterParty = player.characterParty;
            InitializeCharactersUI(characterParty);

            inventoryUI.Initialize(player.Inventory, characterParty);
            characterStatsUI.SetTo(currentSelectedCharacter);
            characterParty.CharacterAdded.Subscribe(CharacterAddedToParty).AddTo(characterPartySubscriptions);
            characterParty.CharacterRemoved.Subscribe(CharacterRemovedFromParty).AddTo(characterPartySubscriptions);
        }

        private void InitializeCharactersUI(IEnumerable<Character> initialCharacterParty)
        {
            freeCharacterPagesIndexes = new Stack<int>(Enumerable.Range(0, allCharacterPages.Length).Reverse());
            var charactersInParty = initialCharacterParty.ToArray();

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
            RemoveCharacterUI(removedCharacter);
            SelectValidCharacterTab(removedCharacter);
        }

        private void SelectValidCharacterTab(Character removedCharacter)
        {
            if (currentSelectedCharacter.Value == null || currentSelectedCharacter.Value != removedCharacter)
            {
                return;
            }

            for (var i = 0; i < allCharacterPages.Length; ++i)
            {
                if (freeCharacterPagesIndexes.Contains(i))
                {
                    continue;
                }

                characterTabs[i].OnPointerClick(null);
                break;
            }
        }

        private void AddCharacterUI(Character character)
        {
            Assert.IsTrue(freeCharacterPagesIndexes.Any(), "Exceeded maximum number of character pages UI.");
            var index = freeCharacterPagesIndexes.Pop();
            characterTabs[index].SetIcon(character.Portrait);
            characterTabs[index].Enable();
            characterTabs[index].transform.SetSiblingIndex(characterParty.Count);
            allCharacterPages[index].Set(player, character);
            usedCharacterPagesMapping.Add(character, index);
        }

        private void RemoveCharacterUI(Character character)
        {
            if (usedCharacterPagesMapping.TryGetValue(character, out var index))
            {
                characterTabs[index].Disable();
                characterTabs[index].transform.SetSiblingIndex(characterParty.Count + 1);
                allCharacterPages[index].Clear();
                freeCharacterPagesIndexes.Push(index);
                usedCharacterPagesMapping.Remove(character);
            }
            else
            {
                Debug.LogError($"No character UI found for character {character.Id}");
            }
        }

        private void CharacterPageSelected(Character character)
        {
            currentSelectedCharacter.Value = character;
        }

        public void Toggle()
        {
            if (isVisible)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
        
        public void Show()
        {
            isVisible = true;
            canvasGroup.SetActive(true);
        }

        public void Hide()
        {
            isVisible = false;
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