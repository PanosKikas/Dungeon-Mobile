using System;
using System.Collections.Generic;
using UnityEngine;
using DMT.Characters;
using System.Linq;
using UniRx;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace DMT.Characters.Inventory.UI
{
    public class InventoryPanel : MonoBehaviour
    {
        private readonly Dictionary<ItemSlot, ItemSlotUI> usedSlots = new();
        private List<ItemSlotUI> freeSlots = new();

        [SerializeField] 
        private Transform slotsParent;
        
        [SerializeField] 
        private ItemDetailsPanel itemDetails;

        [SerializeField]
        private CharacterSelectPopup characterSelectPopupPrefab;

        private readonly Dictionary<ItemSlotUI, IList<IDisposable>> slotSubscriptions = new();
        private readonly List<IDisposable> inventorySubscriptions = new();

        private IInventory inventory;

        private ItemSlotUI currentSelectedSlot;
        private CharacterParty characterParty;
        private List<Character> validUseCharacters = new();
        private IUsable itemAwaitingToBeUsed;

        private GameObject _cachedPopupParent;
        private GameObject popupParent
        {
            get
            {
                if (_cachedPopupParent == null)
                {
                    _cachedPopupParent = GameObject.FindWithTag("Popups");
                }

                return _cachedPopupParent;
            }
        }

        private IDisposable awaitingCharacterSelectSubscription;

        private void Awake()
        {
            freeSlots = slotsParent.GetComponentsInChildren<ItemSlotUI>().ToList();
        }

        public void Initialize(IInventory inventoryModel, CharacterParty characters)
        {
            Assert.IsNotNull(inventoryModel, "Inventory passed to Inventory UI was null");
            Assert.IsNotNull(characters, "Character party passed to Inventory UI was null");
            inventory = inventoryModel;
            characterParty = characters;
            inventorySubscriptions.DisposeAndClear();
            inventory.InventoryItems.ObserveAdd()
                .Subscribe(x => OnItemSlotAdded(x.Value)).AddTo(inventorySubscriptions);
            inventory.InventoryItems.ObserveRemove()
                .Subscribe(x => OnItemSlotRemoved(x.Value)).AddTo(inventorySubscriptions);
        }

        private void OnItemSlotAdded(ItemSlot slot)
        {
            var slotUI = freeSlots.First();
            freeSlots.Remove(slotUI);
            
            slotUI.InitializeTo(slot);
            usedSlots.Add(slot, slotUI);
            
            SubscribeToItemSlotEvents(slotUI);
            slotUI.transform.SetSiblingIndex(usedSlots.Count() - 1);
        }

        private void OnItemSlotRemoved(ItemSlot slot)
        {
            var slotUI = usedSlots[slot];

            if (currentSelectedSlot == slotUI)
            {
                itemDetails.EmptyDescription();
            }

            UnsubscribeFromItemSlotEvents(slotUI);
            slotUI.Clear();
            freeSlots.Add(slotUI);
            usedSlots.Remove(slot);
            slotUI.transform.SetSiblingIndex(usedSlots.Count() + 1);
        }

        private void SubscribeToItemSlotEvents(ItemSlotUI itemSlotUI)
        {
            if (slotSubscriptions.TryGetValue(itemSlotUI, out var oldSubscriptions))
            {
                Debug.LogWarning($"Already subscribed to {itemSlotUI.gameObject.name} slot UI events.");
                oldSubscriptions.DisposeAndClear();
                slotSubscriptions.Remove(itemSlotUI);
            }

            var disposables = new List<IDisposable>();
            itemSlotUI.OnClicked.Subscribe(OnItemSlotClicked).AddTo(disposables);
            itemSlotUI.OnHeld.Subscribe(OnItemSlotHeld).AddTo(disposables);
            slotSubscriptions.Add(itemSlotUI, disposables);
        }

        private void UnsubscribeFromItemSlotEvents(ItemSlotUI itemSlotUI)
        {
            if (slotSubscriptions.TryGetValue(itemSlotUI, out var disposables))
            {
                disposables.DisposeAndClear();
            }
            else
            {
                Debug.LogWarning($"Could not find slot UI subscription events to unsubscribe from {itemSlotUI.gameObject.name}");
            }
        }

        private void OnItemSlotClicked(ItemSlotUI itemSlotUI)
        {
            Assert.IsNotNull(itemSlotUI, "Item slot clicked is null, this should never happen.");

            currentSelectedSlot = itemSlotUI;
            itemDetails.ShowDetailsFor(currentSelectedSlot.Item);
        }

        private void OnItemSlotHeld(ItemSlotUI itemSlotUI)
        {
            Assert.IsNotNull(itemSlotUI, "Item slot held is null, this should never happen.");

            validUseCharacters.Clear();

            if (itemSlotUI.Item is IUsable usable)
            {
                validUseCharacters = characterParty.Where(usable.CanBeUsedOn).ToList();
            }

            if (!validUseCharacters.Any())
            {
                itemSlotUI.InvalidUse();
                return;
            }

            itemAwaitingToBeUsed = itemSlotUI.Item as IUsable;
            awaitingCharacterSelectSubscription?.Dispose();
            
            var popup = Instantiate(characterSelectPopupPrefab, popupParent.transform);
            awaitingCharacterSelectSubscription = popup.CharacterSelected.Subscribe(CharacterSelected);
            popup.Initialize(validUseCharacters, itemSlotUI.transform.position);
        }

        private void CharacterSelected(Character character)
        {
            awaitingCharacterSelectSubscription?.Dispose();

            Assert.IsTrue(validUseCharacters.Any() && itemAwaitingToBeUsed != null, 
                $"Character {character.NameId} selected but item awaiting to be used was null.");
            character.Use(itemAwaitingToBeUsed);
            itemAwaitingToBeUsed = null;
            validUseCharacters.Clear();
        }

        private void OnDestroy()
        {
            foreach (var slotSubscriptionList in slotSubscriptions.Values)
            {
                slotSubscriptionList.DisposeAndClear();
            }

            inventorySubscriptions.DisposeAndClear();
        }
    }
}