using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DMT.Characters;
using System.Linq;
using UniRx;

public class InventoryPanel : MonoBehaviour
{
    private Dictionary<ItemSlot, ItemSlotUI> usedSlots = new();
    private List<ItemSlotUI> freeSlots = new();

    [SerializeField] private Transform slotsParent;

    [SerializeField] private ItemDetailsPanel itemDetails;

    private readonly List<IDisposable> subscriptions = new();

    private readonly Dictionary<ItemSlotUI, IList<IDisposable>> slotSubscriptions = new();
    private IInventory inventory;
    private Character selectedCharacter => characters.First();
    private ItemSlotUI currentSelectedSlot;
    private IEnumerable<Character> characters;

    private void Awake()
    {
        freeSlots = slotsParent.GetComponentsInChildren<ItemSlotUI>().ToList();
    }

    public void Initialize(IInventory inventoryModel, IEnumerable<Character> characters)
    {
        if (inventoryModel == null)
        {
            Debug.LogError("Character does not contain an inventory");
            return;
        }

        inventory = inventoryModel;
        this.characters = characters;
        SubscribeTo(inventoryModel);
    }

    private void SubscribeTo(IInventory inventory)
    {
        subscriptions.Clear();
        this.inventory = inventory;
        inventory.InventoryItems.ObserveAdd()
            .Subscribe(x => OnItemSlotAdded(x.Value)).AddTo(subscriptions);
        inventory.InventoryItems.ObserveRemove()
            .Subscribe(x => OnItemSlotRemoved(x.Value)).AddTo(subscriptions);
    }

    private void OnItemSlotAdded(ItemSlot slot)
    {
        var slotUIToUse = freeSlots.First();
        freeSlots.Remove(slotUIToUse);
        slotUIToUse.InitializeTo(slot);
        usedSlots.Add(slot, slotUIToUse);
        SubscribeToItemSlotEvents(slotUIToUse);
        slotUIToUse.transform.SetSiblingIndex(usedSlots.Count() - 1);
    }

    private void OnItemSlotRemoved(ItemSlot slot)
    {
        var slotUI = usedSlots[slot];
        
        if (currentSelectedSlot == slotUI)
        {
            itemDetails.EmptyDescription();
        }

        UnsubscribeFromItemSlotEvents(slotUI);
        slotUI.Empty();
        freeSlots.Add(slotUI);
        usedSlots.Remove(slot);
        slotUI.transform.SetSiblingIndex(usedSlots.Count() + 1);
    }

    private void SubscribeToItemSlotEvents(ItemSlotUI itemSlotUi)
    {
        if (slotSubscriptions.TryGetValue(itemSlotUi, out var oldSubscriptions))
        {
            Debug.LogError("Already subscribed to this slot.");
            oldSubscriptions.DisposeAndClear();
            slotSubscriptions.Remove(itemSlotUi);
        }
        
        List<IDisposable> disposables = new List<IDisposable>();
        
        itemSlotUi.OnClicked.Subscribe(OnItemSlotClicked).AddTo(disposables);
        itemSlotUi.OnHeld.Subscribe(OnItemSlotHeld).AddTo(disposables);
        slotSubscriptions.Add(itemSlotUi, disposables);
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
        if (itemSlotUI.Item == null)
        {
            throw new InvalidOperationException("Item slot clicked is null");
        }

        currentSelectedSlot = itemSlotUI;
        itemDetails.ShowDetailsFor(currentSelectedSlot.Item);
    }

    private void OnItemSlotHeld(ItemSlotUI itemSlotUI)
    {
        if (itemSlotUI.Item == null)
        {
            throw new InvalidOperationException("Item slot held is null");
        }

        if (itemSlotUI.Item is IUsable usable && usable.TryUseOn(selectedCharacter))
        {
            inventory.RemoveItem(itemSlotUI.Item);
        }
        else
        {
            itemSlotUI.Shake();
        }
    }

    private void OnDestroy()
    {
        foreach (var slotSubscription in slotSubscriptions.Values)
        {
            slotSubscription.DisposeAndClear();
        }
  
        subscriptions.DisposeAndClear();
    }
}