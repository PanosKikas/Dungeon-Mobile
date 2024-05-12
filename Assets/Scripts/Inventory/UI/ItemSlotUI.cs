using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Image itemIcon;
    Text stackText;

    RectTransform rect;

    private ItemSlot itemSlot;
    public IStorable Item => itemSlot.Item;
    [SerializeField] private CanvasGroup itemCanvasGroup;

    bool isPointerDown = false;

    float pointerDownTimer = 0f;

    [SerializeField]
    float requiredHoldTime = 0.7f;

    [SerializeField]
    private Image fillImage;

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

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isPointerDown)
        {
            return;
        }

        if (pointerDownTimer < requiredHoldTime)
        {
            int index = transform.GetSiblingIndex();
            if (Item != null)
            {
                OnClicked?.Invoke(this);
            }
        }
        ResetClick();
    }

    private void Update()
    {
        if (itemSlot == null)
        {
            return;
        }

        if (isPointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= requiredHoldTime)
            {
                ResetClick();
                OnHeld?.Invoke(this);
            }

            fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        }
    }

    private void ResetClick()
    {
        isPointerDown = false;
        pointerDownTimer = 0f;
        fillImage.fillAmount = 0;
    }
}
