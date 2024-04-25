using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon;
    Text stackText;
    ItemClickUI itemClick;
    InventoryUI inventoryGUI;
    RectTransform rect;

    ItemSlot itemSlot;
    [SerializeField] private CanvasGroup itemCanvasGroup;
    public event Action<ItemSlot> ItemSlotClicked;

    private void Awake()
    {
        stackText = GetComponentInChildren<Text>();
        itemClick = GetComponent<ItemClickUI>();
        inventoryGUI = GetComponentInParent<InventoryUI>();
        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        InitializeSlot();   
    }

    public void SubscribeToSlot(ItemSlot slot)
    {
        slot.OnItemChanged += SlotItemChanged;
        slot.OnItemChanged += SlotItemRemoved;
        slot.OnStackChanged += SlotStackCountChanged;
    }
    private void SlotItemChanged(ItemSlot slot)
    {
        itemClick.OnTap.AddListener((_) => ItemSlotClicked?.Invoke(slot));
        SetToItem(slot.Item);
        UpdateStackText(slot.Stack);
    }

    private void SlotItemRemoved(ItemSlot slot)
    {
        //itemClick.OnTap.RemoveListener(inventoryGUI.DisplayItemOnDescription);
    }

    private void SlotStackCountChanged(ItemSlot slot)
    {
        UpdateStackText(slot.Stack);
    }

    void InitializeSlot()
    {
        
       // itemClick.OnLongClick.AddListener(Inventory.Instance.TryUseOnIndex);
    }
        

    public void CannotBeUsedAnimate()
    {
        rect.DOShakePosition(.5f, 2, 40);
    }

    public void SetToItem(IStorable item)
    {
        itemCanvasGroup.alpha = 1f;
        itemIcon.sprite = item.Icon;
    }

    void EnableItemSlotComponents()
    {
        itemClick.enabled = true;
        itemIcon.enabled = true;
        stackText.enabled = true;
    }

    public void RemoveItemFromSlot()
    {
       // DisableItemSlotComponents();
    }

    public void UpdateStackText(int stackCount)
    {
        stackText.text = string.Format("x{0}", stackCount);
    }
}
