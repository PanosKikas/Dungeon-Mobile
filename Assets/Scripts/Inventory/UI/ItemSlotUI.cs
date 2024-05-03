using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Image itemIcon;
    Text stackText;
    ItemClickUI itemClick;
    InventoryUI inventoryGUI;
    RectTransform rect;

    ItemSlot itemSlot;
    [SerializeField] private CanvasGroup itemCanvasGroup;

    bool pointerDown = false;

    float pointerDownTimer = 0f;

    [SerializeField]
    float requiredHoldTime;

    [SerializeField]
    private Image fillImage;

    public event Action<ItemSlot> OnClicked;

    public event Action<ItemSlot> OnHeld;

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

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (pointerDownTimer < requiredHoldTime)
        {
            int index = transform.GetSiblingIndex();
            OnClicked?.Invoke(itemSlot);
        }
        ResetClick();
    }

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= requiredHoldTime)
            {
                OnHeld?.Invoke(itemSlot);
                ResetClick();
            }

            fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        }
    }

    private void ResetClick()
    {
        pointerDown = false;
        pointerDownTimer = 0f;
        fillImage.fillAmount = 0;
    }
}
