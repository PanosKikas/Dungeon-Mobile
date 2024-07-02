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

        [SerializeField] private Transform slotsParent;

        [SerializeField] private ItemDetailsPanel itemDetails;

        [FormerlySerializedAs("characterSelectPopup")] [SerializeField]
        private CharacterSelectPopup characterSelectPopupPrefab;

        private readonly Dictionary<ItemSlotUI, IList<IDisposable>> slotSubscriptions = new();
        private readonly List<IDisposable> inventorySubscriptions = new();

        private IInventory inventory;

        private ItemSlotUI currentSelectedSlot;
        private CharacterParty characterParty;
        private List<Character> validUseCharacters = new();
        private IUsable itemAwaitingToBeUsed;
        private IDisposable awaitingCharacterSelectSubscription;

        private void Awake()
        {
            freeSlots = slotsParent.GetComponentsInChildren<ItemSlotUI>().ToList();
        }

        public void InitializeTo(IInventory inventoryModel, CharacterParty characters)
        {
            Assert.IsNotNull(inventoryModel);

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

        private void SubscribeToItemSlotEvents(ItemSlotUI slotUI)
        {
            if (slotSubscriptions.TryGetValue(slotUI, out var oldSubscriptions))
            {
                Debug.LogError("Already subscribed to this slot.");
                oldSubscriptions.DisposeAndClear();
                slotSubscriptions.Remove(slotUI);
            }

            List<IDisposable> disposables = new List<IDisposable>();

            slotUI.OnClicked.Subscribe(OnItemSlotClicked).AddTo(disposables);
            slotUI.OnHeld.Subscribe(OnItemSlotHeld).AddTo(disposables);
            slotSubscriptions.Add(slotUI, disposables);
        }

        private void UnsubscribeFromItemSlotEvents(ItemSlotUI itemSlotUI)
        {
            if (slotSubscriptions.TryGetValue(itemSlotUI, out var disposables))
            {
                disposables.DisposeAndClear();
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
                validUseCharacters = FindAllCharactersThatCanUse(usable).ToList();
            }

            if (!validUseCharacters.Any())
            {
                itemSlotUI.InvalidUse();
                return;
            }

            itemAwaitingToBeUsed = itemSlotUI.Item as IUsable;
            awaitingCharacterSelectSubscription?.Dispose();
            var popupParent = GameObject.FindWithTag("Popups");
            CharacterSelectPopup popup = Instantiate(characterSelectPopupPrefab, popupParent.transform);
            awaitingCharacterSelectSubscription = popup.CharacterSelected.Subscribe(CharacterSelected);
            popup.InitializeTo(validUseCharacters, itemSlotUI.transform.position + Vector3.up * 75f);
        }

        private IEnumerable<Character> FindAllCharactersThatCanUse(IUsable usable)
        {
            return characterParty.Where(usable.CanBeUsedOn);
        }

        private void CharacterSelected(Character character)
        {
            awaitingCharacterSelectSubscription?.Dispose();

            Assert.IsTrue(validUseCharacters.Any() && itemAwaitingToBeUsed != null);
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