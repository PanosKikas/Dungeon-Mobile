using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon;
    Text stackText;

    private RectTransform rect;

    private ItemSlot itemSlot;
    public IStorable Item => itemSlot.Item;
    [SerializeField] private CanvasGroup itemCanvasGroup;

    public event Action<ItemSlotUI> OnClicked;

    public event Action<ItemSlotUI> OnHeld;

    private void Awake()
    {
        stackText = GetComponentInChildren<Text>();
        rect = GetComponent<RectTransform>();
    }

    public void Shake()
    {
        rect.DOShakePosition(.5f, 2, 40);
    }

    public void InitializeTo(ItemSlot slot)
    {
        var item = slot.Item;
        itemSlot = slot;
        itemIcon.sprite = item.Icon;
        UpdateUI();
        EnableInteraction();
    }

    public void UpdateUI()
    {
        UpdateStackText(itemSlot.Stack);
    }

    public void Empty()
    {
        itemIcon.sprite = null;
        DisableInteraction();
    }

    public void EnableInteraction()
    {
        itemCanvasGroup.alpha = 1f;
        itemCanvasGroup.interactable = true;
    }

    public void DisableInteraction()
    {
        itemCanvasGroup.alpha = 0f;
        itemCanvasGroup.interactable = false;
    }

    public void UpdateStackText(int stackCount)
    {
        stackText.text = string.Format("x{0}", stackCount);
    }

    public void SlotHeld()
    {
        OnHeld?.Invoke(this);
    }

    public void SlotClicked()
    {
        OnClicked?.Invoke(this);
    }
}
