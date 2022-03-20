using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemSlotUI : MonoBehaviour
{
    Image itemIcon;
    Text stackText;
    ItemClickUI itemClick;
    InventoryGUI inventoryGUI;
    RectTransform rect;

    StoredItem item;

    private void Awake()
    {
        itemIcon = transform.GetChild(0).GetComponent<Image>();
        stackText = GetComponentInChildren<Text>();
        itemClick = GetComponent<ItemClickUI>();
        inventoryGUI = GetComponentInParent<InventoryGUI>();
        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        InitializeSlot();   
    }

    void InitializeSlot()
    {
        itemClick.OnTap.AddListener(inventoryGUI.DisplayItemOnDescription);
        itemClick.OnLongClick.AddListener(Inventory.Instance.TryUseOnIndex);
        DisableItemSlotComponents();
    }
        

    public void CannotBeUsedAnimate()
    {
        rect.DOShakePosition(.5f, 2, 40);
    }

    public void ShowItemOnSlot(StoredItem item)
    {
        this.item = item;
        EnableItemSlotComponents();
        itemIcon.sprite = item.Item.Icon;
        UpdateStackText();
    }

    void EnableItemSlotComponents()
    {
        itemClick.enabled = true;
        itemIcon.enabled = true;
        stackText.enabled = true;
    }

    void DisableItemSlotComponents()
    {
        itemClick.enabled = false;
        itemIcon.enabled = false;
        stackText.enabled = false;
    }

    public void RemoveItemFromSlot()
    {
        DisableItemSlotComponents();
        this.item = null;
    }

    public void UpdateStackText()
    {
        stackText.text = string.Format("x{0}", item.Stack);
    }
}
