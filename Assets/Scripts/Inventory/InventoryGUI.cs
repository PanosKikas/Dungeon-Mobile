using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryGUI : MonoBehaviour
{
    
    public GameObject[] itemSlots;
    public Image[] itemIcons;
    public Text[] itemStackText;

    [SerializeField]
    Text itemDescritpion;

    int? lastDisplayedIndex = null;

    private void Awake()
    {

        InitializeUI();

    }

    void InitializeUI()
    {
        itemSlots = new GameObject[transform.childCount];
        itemIcons = new Image[transform.childCount];
        itemStackText = new Text[transform.childCount];

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i] = transform.GetChild(i).gameObject;

            itemIcons[i] = (itemSlots[i]).transform.GetChild(0).GetComponent<Image>();

            ItemClickUI click = itemSlots[i].GetComponent<ItemClickUI>();

            itemStackText[i] = itemIcons[i].GetComponentInChildren<Text>();
            click.OnTap.AddListener(DisplayItemOn);
            click.OnLongClick.AddListener(Inventory.Instance.TryUseOnIndex);
            click.enabled = false;
           
            itemStackText[i].enabled = false;
            itemIcons[i].enabled = false;
        }
    }

    public void DisplayItemOn(int index)
    {
        InventoryPickupSO item = Inventory.Instance.items[index].Item;
       
        itemDescritpion.text = string.Format("{0}: {1}", item.Name, item.description);
        lastDisplayedIndex = index;
    }

    public void NotUsedItem(int index)
    {
       var layoutGroup = GetComponent<GridLayoutGroup>();
       layoutGroup.enabled = false;

       itemSlots[index].GetComponent<RectTransform>().DOShakePosition(.5f, 2, 40);

       layoutGroup.enabled = true;
    
        
    }

    public void ModifyItemGUIOn(int index)
    {
        
        var inventoryPickup = Inventory.Instance.items[index];
        
        if (inventoryPickup == null)
        {
            
            HideItemOn(index);
        }
        else
        {
            ShowItemOn(index);
        }
              
    }

    void ShowItemOn(int index)
    {
        
        var itemEntry = Inventory.Instance.items[index];
        itemSlots[index].GetComponent<ItemClickUI>().enabled = true;
        itemStackText[index].enabled = true;
        itemStackText[index].text = string.Format("x{0}", itemEntry.Stack);
        Image icon = itemIcons[index];
        icon.sprite = itemEntry.Item.Icon;
        icon.enabled = true;
    }

    void HideItemOn(int index)
    {
        itemIcons[index].enabled = false;
        itemStackText[index].enabled = false;
        itemSlots[index].GetComponent<ItemClickUI>().enabled = false;
        if (lastDisplayedIndex != null && lastDisplayedIndex == index)
        {
            itemDescritpion.text = "";

        }
        
    }

    
}
