using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Collections.Generic;
using DMT.Pickups;
using UniRx;

namespace DMT.Characters.Inventory.UI
{
    public class ItemSlotUI : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
        private Text stackText;
        private RectTransform rect;
        private ItemSlot itemSlot;
        public IStorable Item => itemSlot.Item;
        [SerializeField] private CanvasGroup itemCanvasGroup;

        public readonly ISubject<ItemSlotUI> OnClicked = new Subject<ItemSlotUI>();
        public readonly ISubject<ItemSlotUI> OnHeld = new Subject<ItemSlotUI>();

        private readonly List<IDisposable> itemSubscriptions = new();

        private void Awake()
        {
            stackText = GetComponentInChildren<Text>();
            rect = GetComponent<RectTransform>();
        }

        public void InvalidUse()
        {
            rect.DOShakePosition(.5f, 2, 40);
        }

        public void InitializeTo(ItemSlot slot)
        {
            itemSubscriptions.Clear();
            var item = slot.Item;
            itemSlot = slot;
            itemIcon.sprite = item.Icon;
            slot.Stack.Subscribe(UpdateStackText).AddTo(itemSubscriptions);
            EnableInteraction();
        }

        public void Clear()
        {
            itemIcon.sprite = null;
            DisableInteraction();
        }

        private void EnableInteraction()
        {
            itemCanvasGroup.alpha = 1f;
            itemCanvasGroup.interactable = true;
        }

        private void DisableInteraction()
        {
            itemCanvasGroup.alpha = 0f;
            itemCanvasGroup.interactable = false;
        }

        private void UpdateStackText(int stackCount)
        {
            stackText.text = $"x{stackCount}";
        }

        public void SlotHeld()
        {
            OnHeld.OnNext(this);
        }

        public void SlotClicked()
        {
            OnClicked.OnNext(this);
        }

        private void OnDisable()
        {
            itemSubscriptions.DisposeAndClear();
        }
    }
}
